using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public enum FieldType
    {
        X,
        O,
        Empty
    }
    public partial class Field : UserControl
    {
        static List<Field> fields = new List<Field>();
        public static void ClearFields()
        {
            foreach (var field in fields)
            {
                field.Type = FieldType.Empty;
            }
        }
        public Field()
        {
            InitializeComponent();
            fields.Add(this);
        }

        private FieldType _type = FieldType.Empty;
        public FieldType Type
        {
            get => _type;
            set
            {
                if (value == FieldType.X)
                    image.Source = new BitmapImage(new Uri(@"\RC\x.png", UriKind.Relative));
                if (value == FieldType.O)
                    image.Source = new BitmapImage(new Uri(@"\RC\o.png", UriKind.Relative));
                if (value == FieldType.Empty)
                    image.Source = new BitmapImage(new Uri(@"\RC\bg.png", UriKind.Relative));
                _type = value;
            }
        }

        public byte SlotX
        {
            get => (byte)GetValue(SlotXProperty);
            set => SetValue(SlotXProperty, value);
        }
        public static readonly DependencyProperty SlotXProperty = DependencyProperty.Register(nameof(SlotX), typeof(byte), typeof(Field), new UIPropertyMetadata());

        public byte SlotY
        {
            get => (byte)GetValue(SlotYProperty);
            set => SetValue(SlotYProperty, value);
        }
        public static readonly DependencyProperty SlotYProperty = DependencyProperty.Register(nameof(SlotY), typeof(byte), typeof(Field), new UIPropertyMetadata());


    }
}
