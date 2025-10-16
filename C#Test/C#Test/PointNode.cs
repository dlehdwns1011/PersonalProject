using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace C_Test {
    internal class PointNode {
        private PointNode leftPoint;
        private bool isSetLeft = false;
        private PointNode rightPoint;
        private bool isSetRight = false;
        public PointNode downPoint { get; set; }

        public Point Position { get; set; }

        public PointNode(Point position) {
            Position = position;
        }

        public PointNode GetLeft() {
            if (isSetLeft) {
                return leftPoint;
            }

            return null;
        }

        public void SetLeft(PointNode leftNode) {
            leftPoint = leftNode;
            isSetLeft = true;
        }

        public PointNode GetRight() {
            if (isSetRight) {
                return rightPoint;
            }

            return null;
        }

        public void SetRight(PointNode rightNode) {
            rightPoint = rightNode;
            isSetRight = true;
        }

        public void Clear() {
            leftPoint = null;
            rightPoint = null;
            isSetLeft = false;
            isSetRight = false;
        }
    }
}
