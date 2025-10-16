@echo off
REM ============================================
REM PptExtractor 빌드 자동화 배치 파일
REM ============================================

REM 0. bin 폴더 정리
if exist bin (
    echo Removing old bin folder...
    rmdir /s /q bin
)
mkdir bin

REM 1. 클래스 파일 생성
echo Compiling Java sources...
javac -d bin -cp "lib/*" src\PptExtractor.java
if errorlevel 1 (
    echo Compilation failed.
    pause
    exit /b 1
)

REM 2. jar 파일 생성
echo Creating JAR...
jar -cfm PptExtractor.jar manifest.txt -C bin .

if errorlevel 1 (
    echo JAR creation failed.
    pause
    exit /b 1
)

echo Build complete!
pause
