using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Forms;

namespace St.Common
{
    public class ViewFrame : BindableBase
    {
        public ViewFrame(IntPtr viewIntPtr, PictureBox pictureBox)
        {
            Row = 0;
            Column = 0;
            RowSpan = 1;
            ColumnSpan = 1;
            Visibility = Visibility.Collapsed;
            ViewType = 1;
            Hwnd = viewIntPtr;
            IsBigView = false;
            ViewOrder = 0;
            IsOpened = false;
            PictureBox = pictureBox;
        }

        private int row;

        public int Row
        {
            get { return row; }
            set { SetProperty(ref row, value); }
        }

        private int rowSpan;

        public int RowSpan
        {
            get { return rowSpan; }
            set { SetProperty(ref rowSpan, value); }
        }

        private int column;

        public int Column
        {
            get { return column; }
            set { SetProperty(ref column, value); }
        }

        private int columnSpan;

        public int ColumnSpan
        {
            get { return columnSpan; }
            set { SetProperty(ref columnSpan, value); }
        }

        private Visibility visibility;

        public Visibility Visibility
        {
            get { return visibility; }
            set { SetProperty(ref visibility, value); }
        }

        private IntPtr hwnd;

        public IntPtr Hwnd
        {
            get { return hwnd; }
            set { SetProperty(ref hwnd, value); }
        }

        private string phoneId;

        public string PhoneId
        {
            get { return phoneId; }
            set { SetProperty(ref phoneId, value); }
        }

        private int viewType;

        public int ViewType
        {
            get { return viewType; }
            set { SetProperty(ref viewType, value); }
        }

        private string viewName;

        public string ViewName
        {
            get { return viewName; }
            set { SetProperty(ref viewName, value); }
        }

        private bool isBigView;

        public bool IsBigView
        {
            get { return isBigView; }
            set { SetProperty(ref isBigView, value); }
        }

        private int viewOrder;

        public int ViewOrder
        {
            get { return viewOrder; }
            set { SetProperty(ref viewOrder, value); }
        }

        private bool isOpened;

        public bool IsOpened
        {
            get { return isOpened; }
            set { SetProperty(ref isOpened, value); }
        }

        public PictureBox PictureBox { get; set; }
    }
}
