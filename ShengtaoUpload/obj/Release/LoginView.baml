   M S B A M L   `   `   `  ���� D  @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common   	St.Upload   St.Upload.LoginView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  7helper*clr-namespace:St.Common;assembly=St.Common  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  -   (  "MahApps.Metro.Controls.MetroWindow   Name$  LoginV��$	��350��$	��500��   ShowTitleBar .   � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � Topmost .  +� FocusedElement ��  �� ElementName$
 User�� � 	Resources 	 �� 
TargetType
 �� Property �� Value   MaterialDesignTextBox �� BasedOn  MaterialDesignPasswordBox  MaterialDesignCheckBox'  !MaterialDesignThemes.Wpf.PackIcon % !MaterialDesignCircularProgressBar  MaterialDesignRaisedButton��%�  ']�       '�� :     7   'G� �     7  '�� �     7  '  ;    '� �    7  '�� �    7  �� "	 ]�.��� 
 � ��$	 260���� 
 � ��$ 10q��� "	 ��8   .��� 
 � ��$ Center=��� 
 � ��$ 10 5 5 5q��� 
 � ��$ 20(��� "	 G�8   .��� 
 � ��$ Center=��� 
 � ��$ 10 5 5 5q��� 
 � ��$ 20(��� "	 ��8   .��� 
 � ��$ 16(��� 
 � ��$ 	20 5 20 5q��� "	  .��� 
 � ��$ 38���� 
 � ��$ 38���� 
 � ��$ Center=��� "	 �8   .��� 
 � ��$
 Left=��� 
 � ��$ 5 0 0 0q��� "	 ��8   .��� 
 � ��$ 10q��� 
 � ��$ 20(��� 
 � ��$ 40���� 
 � ��$ Center=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+.��� .+�&   MaterialDesignThemes.Wpf.Flipper    MaterialDesignCardFlipper#��[  J� SnapsToDevicePixels .    FrontContent � ���� $ LoginV�� �� Path$ ActualWidth����� $ LoginV��$ ActualHeight� � RowDefinitions �� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��40 �.+�]� $��1��.+�  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind$ Account=����� $
 User��$ BorderBrush��� -    ��Name$
 User��)	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$ 	用户名������ *
UserName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�{��� *IsLoginEnabled+]� $��2��.+�  $	 Key=����� $	 Pwd��$ BorderBrush�G� -    G�Name$	 Pwd��$ 密码��!
   St.Common.PasswordBoxHelper 
  Attach .  
  Password �� *
Password+$ TwoWay=�$ PropertyChanged=�{��� *IsLoginEnabled+]� -    ]�Name$ 	Dockpanel��$��3��.+��� $��记住密码�� d� 	IsChecked �� *
RememberMe+{��� *IsLoginEnabled+�� $��自动登录��$��Right=� �� *	AutoLogin+{��� *IsLoginEnabled+� $��4�� � ColumnDefinitions �� �� $
��Auto ��� .+��� 2  ,System.Windows.Markup.XmlAttributeProperties   XmlSpace  preserve$��	登   录��! �� 	IsDefault! . $��1������ $ 	Dockpanel��$ ActualWidth����� *LoginCommand+{��� *IsLoginEnabled+� " � Value$" 0Q�$��2��# � IsIndeterminate# �� *ShowProgressBar+� $��5��'  !MaterialDesignThemes.Wpf.Snackbar  $��Bottom=����� $ LoginV��$ ActualWidth�$   MessageQueue$ �� *MessageQueue+