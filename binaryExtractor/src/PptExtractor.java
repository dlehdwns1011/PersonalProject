import java.io.FileInputStream;
import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.awt.geom.Rectangle2D;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.Date;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.core.util.DefaultIndenter;
import com.fasterxml.jackson.core.util.DefaultPrettyPrinter;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectWriter;

import org.apache.poi.hslf.model.HeadersFooters;
import org.apache.poi.hslf.usermodel.*;
import org.apache.poi.sl.usermodel.Placeholder;
import org.apache.poi.sl.usermodel.PlaceholderDetails;
import org.apache.poi.sl.usermodel.Shape;
import org.apache.poi.sl.usermodel.TextShape;
import org.apache.poi.sl.usermodel.PictureData.PictureType;
import org.apache.poi.hpsf.SummaryInformation;

public class PptExtractor {

    private static HSLFSlideShow ppt = null;

    public static void main(String[] args) throws IOException {
        Map<String, Object> document = new LinkedHashMap<>();
        List<Map<String, Object>> slidesList = new ArrayList<>();
        document.put("slides", slidesList);

        if (args.length < 2) {
            System.out.println("Usage: java PptExtractor <ppt-file> <output-json>");
            return;
        }

        String filePath = args[0];
        try (FileInputStream fis = new FileInputStream(filePath)) {
            ppt = new HSLFSlideShow(fis);

            for (HSLFSlide slide : ppt.getSlides()) {
                Map<String, Object> slideObject = new LinkedHashMap<>();
                List<ShapeInfo> slideShapes = new ArrayList<>();
                HashSet<String> masterPlaceholdersChecker = new HashSet<>();

                for (HSLFShape shape : slide.getShapes()) {
                    ExtractShapes(shape, slide, slideShapes, masterPlaceholdersChecker);
                }

                HSLFMasterSheet master = slide.getMasterSheet();
                if (master != null) {
                    for (HSLFShape masterShape : master.getShapes()) {
                        if (isMasterShapeVisible(masterShape, masterPlaceholdersChecker, slide)) {
                            ExtractShapes(masterShape, slide, slideShapes, null);
                        }
                    }
                }

                slideShapes.sort(Comparator.comparingDouble((ShapeInfo si) -> si.anchor.getY())
                                          .thenComparingDouble(si -> si.anchor.getX()));

                List<Map<String, Object>> shapesList = new ArrayList<>();
                for (ShapeInfo info : slideShapes) {
                    shapesList.add(info.toJson());
                }

                slideObject.put("shapes", shapesList);
                slidesList.add(slideObject);
            }
        }

        ObjectMapper objectMapper = new ObjectMapper();
        objectMapper.setSerializationInclusion(JsonInclude.Include.NON_NULL);
        DefaultPrettyPrinter prettyPrinter = new DefaultPrettyPrinter();
        prettyPrinter.indentArraysWith(DefaultIndenter.SYSTEM_LINEFEED_INSTANCE);
        ObjectWriter writer = objectMapper.writer(prettyPrinter);

        String outputPath = args[1];
        try {
            writer.writeValue(new File(outputPath), document);
        } catch (IOException e) {
            System.out.println("오류 발생: " + e.getMessage());
        }

        System.out.println("pptextractor 성공");
    }

    private static void ExtractShapes(Shape<?,?> shape, HSLFSlide slide, List<ShapeInfo> collector, HashSet<String> placeholderTracker) {
        if (shape instanceof HSLFTable) {
            collector.add(new TableInfo((HSLFTable) shape));
            return;
        }

        if (shape instanceof HSLFGroupShape) {
            HSLFGroupShape group = (HSLFGroupShape) shape;
            Rectangle2D groupAnchor = group.getAnchor();
            Rectangle2D interiorAnchor = group.getInteriorAnchor();

            double scaleX = groupAnchor.getWidth() / interiorAnchor.getWidth();
            double scaleY = groupAnchor.getHeight() / interiorAnchor.getHeight();

            for (HSLFShape child : group.getShapes()) {
                Rectangle2D childAnchor = child.getAnchor();
                if (childAnchor != null) {
                    Rectangle2D newAnchor = new Rectangle2D.Double(
                        groupAnchor.getX() + (childAnchor.getX() - interiorAnchor.getX()) * scaleX,
                        groupAnchor.getY() + (childAnchor.getY() - interiorAnchor.getY()) * scaleY,
                        childAnchor.getWidth() * scaleX,
                        childAnchor.getHeight() * scaleY
                    );
                    child.setAnchor(newAnchor);
                }
                ExtractShapes(child, slide, collector, placeholderTracker);
            }
            return;
        }

        if (shape instanceof HSLFPictureShape) {
            collector.add(new ImageInfo((HSLFPictureShape) shape));
            return;
        }

        if (shape instanceof TextShape) {
            TextBoxInfo textInfo = new TextBoxInfo((TextShape<?,?>) shape, slide);
            if (!textInfo.paragraphs.isEmpty() || textInfo.shapeHyperlinkAddress != null) {
                collector.add(textInfo);
            }
        }

        if (placeholderTracker != null && shape instanceof TextShape) {
            PlaceholderDetails details = ((TextShape<?,?>) shape).getPlaceholderDetails();
            if (details != null && details.getPlaceholder() != null) {
                placeholderTracker.add(details.getPlaceholder().name());
            }
        }
    }
    
    private static boolean isMasterShapeVisible(Shape<?,?> masterShape, HashSet<String> slidePlaceholders, HSLFSlide slide) {
        if (masterShape instanceof TextShape) {
            TextShape<?,?> textShape = (TextShape<?,?>) masterShape;
            PlaceholderDetails details = textShape.getPlaceholderDetails();

            if (details != null && details.getPlaceholder() != null) {
                String placeholderName = details.getPlaceholder().name();
                if (slidePlaceholders.contains(placeholderName)) {
                    return false;
                }

                var hf = slide.getHeadersFooters();
                if (hf == null) {
                    return false;
                }

                Placeholder p = details.getPlaceholder();
                if (p == Placeholder.DATETIME && !hf.isDateTimeVisible()) return false;
                if (p == Placeholder.SLIDE_NUMBER && !hf.isSlideNumberVisible()) return false;
                if (p == Placeholder.FOOTER && !hf.isFooterVisible()) return false;
                if (p == Placeholder.TITLE || p == Placeholder.BODY || p == Placeholder.SUBTITLE) return false;
            }
        }
        return true;
    }

    private static Date getLastSaveDate() {
        SummaryInformation si = ppt.getSummaryInformation();
        if (si != null) return si.getLastSaveDateTime();
        return null;
    }
    
    private static String getTableText(HSLFTable table) {
        StringBuilder sb = new StringBuilder();
        for (int r = 0; r < table.getNumberOfRows(); r++) {
            for (int c = 0; c < table.getNumberOfColumns(); c++) {
                HSLFTableCell cell = table.getCell(r, c);
                if (cell != null && !cell.isMerged()) {
                    var rowSpan = cell.getRowSpan();
                    String row = String.valueOf(r) + ((rowSpan > 1) ? "-" + (r + rowSpan - 1) : "");
                    var colSpan = cell.getGridSpan();
                    String col = String.valueOf(c) + ((colSpan > 1) ? "-" + (c + colSpan - 1) : "");
                    if (cell.getText() != null) {
                        sb.append("(" + row + "," + col + ")" + cell.getText());
                    }
                    sb.append("\n");
                }
            }
        }
        return sb.toString().trim();
    }

    private static String getShapeType(Shape<?,?> shape) {
        if (shape instanceof HSLFTable) return "Table";
        if (shape instanceof HSLFTextBox) return "TextBox";
        if (shape instanceof HSLFAutoShape) return "AutoShape";
        if (shape instanceof HSLFPictureShape) return "Picture";
        if (shape instanceof HSLFGroupShape) return "Group";
        return shape.getClass().getSimpleName();
    }

    // ======================== Base =========================
    private static abstract class ShapeInfo {
        Rectangle2D anchor;
        String type;
        ShapeInfo(Shape<?,?> shape) {
            this.anchor = shape.getAnchor();
            this.type = getShapeType(shape);
        }
        abstract Map<String,Object> toJson();
    }

    // ======================== TextBox =========================
    private static class TextBoxInfo extends ShapeInfo {
        String shapeHyperlinkAddress;
        String shapeHyperlinkLabel;
        List<Map<String,Object>> paragraphs = new ArrayList<>();

        TextBoxInfo(TextShape<?,?> shape, HSLFSlide slide) {
            super(shape);

            var shapeLink = shape.getHyperlink();
            if (shapeLink != null) {
                shapeHyperlinkAddress = shapeLink.getAddress();
                shapeHyperlinkLabel = shapeLink.getLabel();
            }

            int paraIndex = 0;
            List<Map<String,Object>> rawRuns = new ArrayList<>();

            for (var para : shape.getTextParagraphs()) {
                String bullet = null;
                if (para.getBulletStyle() != null) {
                    bullet = para.getBulletStyle().getBulletCharacter();
                }

                for (var run : para.getTextRuns()) {
                    String text = run.getRawText();

                    var placeholderInfo = shape.getPlaceholderDetails();
                    if (placeholderInfo != null && shape.getPlaceholder() != null) {
                        text = GetHeadersFootersString(shape, slide);
                    }

                    var runLink = run.getHyperlink();

                    Map<String,Object> runJson = new LinkedHashMap<>();
                    runJson.put("paragraphIndex", paraIndex);
                    runJson.put("text", text);
                    if (bullet != null) runJson.put("bullet", bullet);
                    if (runLink != null) {
                        Map<String,Object> linkJson = new LinkedHashMap<>();
                        linkJson.put("address", runLink.getAddress());
                        linkJson.put("label", runLink.getLabel());
                        runJson.put("hyperlink", linkJson);
                    }
                    rawRuns.add(runJson);
                }
                paraIndex++;
            }

            // === Run 병합 로직 ===
            Map<String,Object> prev = null;
            for (Map<String,Object> run : rawRuns) {
                if (prev != null &&
                    prev.get("paragraphIndex").equals(run.get("paragraphIndex")) &&
                    ((prev.get("hyperlink") == null && run.get("hyperlink") == null) ||
                    (prev.get("hyperlink") != null && prev.get("hyperlink").equals(run.get("hyperlink")))) &&
                    ((prev.get("bullet") == null && run.get("bullet") == null) ||
                    (prev.get("bullet") != null && prev.get("bullet").equals(run.get("bullet"))))
                ) {
                    // 같은 paragraph, 같은 hyperlink, 같은 bullet이면 text 합치기
                    prev.put("text", prev.get("text").toString() + run.get("text").toString());
                } else {
                    paragraphs.add(run);
                    prev = run;
                }
            }
        }

        String GetHeadersFootersString(TextShape<?,?> shape, HSLFSlide slide) {
            var placeholderInfo = shape.getPlaceholderDetails();
            if (placeholderInfo != null && shape.getPlaceholder() != null) {
                HeadersFooters hf = slide.getHeadersFooters();
                if (hf == null) {
                    return "";
                }

                Placeholder placeholder = shape.getPlaceholder();
                switch (placeholder) {
                    case DATETIME:
                        return hf.isDateTimeVisible() ? (hf.getDateTimeText() != null ? hf.getDateTimeText() : new SimpleDateFormat("yyyy-MM-dd").format(getLastSaveDate())) : "";
                    
                    case SLIDE_NUMBER:
                        return hf.isSlideNumberVisible() ? String.valueOf(slide.getSlideNumber()) : "";

                    case FOOTER:
                        return hf.isFooterVisible() ? hf.getFooterText() : "";
                                        
                    default:
                        return "";
                }
            }

            return "";
        }

        @Override
        Map<String,Object> toJson() {
            Map<String,Object> obj = new LinkedHashMap<>();
            Map<String,Object> shapeObj = new LinkedHashMap<>();
            shapeObj.put("contents", paragraphs);

            if (shapeHyperlinkAddress != null) {
                Map<String,Object> linkJson = new LinkedHashMap<>();
                linkJson.put("address", shapeHyperlinkAddress);
                linkJson.put("label", shapeHyperlinkLabel);
                shapeObj.put("shapeHyperlink", linkJson);
            }

            obj.put("TextBox", shapeObj);
            return obj;
        }
    }


    // ======================== Image =========================
    private static class ImageInfo extends ShapeInfo {
        int pictureIndex;
        PictureType pictureType;
        String shapeHyperlinkAddress;
        String shapeHyperlinkLabel;

        ImageInfo(HSLFPictureShape shape) {
            super(shape);
            HSLFPictureData data = shape.getPictureData();
            this.pictureIndex = data.getIndex();
            this.pictureType = data.getType();

            HSLFHyperlink link = shape.getHyperlink();
            if (link != null) {
                shapeHyperlinkAddress = link.getAddress();
                shapeHyperlinkLabel = link.getLabel();
            }
        }

        @Override
        Map<String,Object> toJson() {
            Map<String,Object> obj = new LinkedHashMap<>();
            Map<String,Object> imgObj = new LinkedHashMap<>();
            imgObj.put("file", "image" + pictureIndex + "." + pictureType.toString().toLowerCase());

            if (shapeHyperlinkAddress != null) {
                Map<String,Object> linkJson = new LinkedHashMap<>();
                linkJson.put("address", shapeHyperlinkAddress);
                linkJson.put("label", shapeHyperlinkLabel);
                imgObj.put("shapeHyperlink", linkJson);
            }

            obj.put("Picture", imgObj);
            return obj;
        }
    }

    // ======================== Table =========================
    private static class TableInfo extends ShapeInfo {
        String tableText;

        TableInfo(HSLFTable table) {
            super(table);
            this.tableText = getTableText(table);
        }

        @Override
        Map<String,Object> toJson() {
            Map<String,Object> obj = new LinkedHashMap<>();
            Map<String,Object> tableObj = new LinkedHashMap<>();
            tableObj.put("contents", tableText.split("\n"));
            obj.put("Table", tableObj);
            return obj;
        }
    }
}
