   M S B A M L   `   `   `  ����D  @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common  5       	St.Upload   St.Upload.LoginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      7helper*clr-namespace:St.Common;assembly=St.Common  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  LoginV��5      $	��350��5      $	��500��5         ShowTitleBar .   � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � ShowInTaskbar .   � Topmost .  +� FocusedElement 5      ��  �� ElementName$
 User�� � 	Resources 5      	 �� 
TargetType
 �� Property �� Value   MaterialDesignTextBox �� BasedOn  MaterialDesignPasswordBox  MaterialDesignCheckBox'  !MaterialDesignThemes.Wpf.PackIcon % !MaterialDesignCircularProgressBar  MaterialDesignRaisedButton��%_  ']�       '�� m     7   'G�     7  '�� �    7  '  -    '� �    7  '�� 9    7  �� 5   
   "	 ]�6   .�5      �� 
 � ��$	 260��6&   �� 5      
 � ��$ 10q�6'   5      �� 5   
   "	 ��6   8   .�5      �� 
 � ��$ Center=�62   �� 5      
 � ��$ 10 5 5 5q�6'   �� 5      
 � ��$ 20(�6)   66   5       �� 5"   
   "	 G�6   8   .�5#      �� 
 � ��$ Center=�62   �� 5$      
 � ��$ 10 5 5 5q�6'   �� 5%      
 � ��$ 20(�6)   66   5&      �� 5(   
   "	 ��6   8   .�5)      �� 
 � ��$ 16(�6)   �� 5*      
 � ��$ 	20 5 20 5q�6'   5+      �� 5-   
   "	  6   .�5.      �� 
 � ��$ 38��6&   �� 5/      
 � ��$ 38��6'   �� 50      
 � ��$ Center=�62   51      �� 53   
   "	 �6   8   .�54      �� 
 � ��$
 Left=�64   �� 55      
 � ��$ 5 0 0 0q�6'   56      �� 58   
   "	 ��6   8   .�59      �� 
 � ��$ 10q�6'   �� 5:      
 � ��$ 20(�6)   66   �� 5;      
 � ��$ 40��6'   64   �� 5<      
 � ��$ Center=�69   6J   5=      5>      .  (System.Windows.Interactivity.Interaction   Triggers 5@      /  )System.Windows.Interactivity.EventTrigger  5A   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5B      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+5C      .��5F      � .+�5G   
   &   MaterialDesignThemes.Wpf.Flipper    MaterialDesignCardFlipper#��[ 6    J� SnapsToDevicePixels .    FrontContent 5H      � 5I      ��6   �� $ LoginV�� �� Path$ ActualWidth���6M   �� $ LoginV��$ ActualHeight� � RowDefinitions 5J      �� 5K      �� 5L      $
��Auto �6(   �� 5M      $
��Auto �6(   �� 5N      $
��Auto �6(   �� 5O      $
��Auto �6(   �� 5P      $��40 �6(   .+�5S      ]� $��1��6    .+�5T        �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind$ Account=�6&   ��5U      �� $
 User��$ BorderBrush��� 5W      -   5         ��Name$
 User��5W   "   )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$ 	用户名��5X      ��5Y      �� *
UserName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�{�5Z      �� *IsLoginEnabled+5[      ]� 5]      $��2��6    .+�5^        $	 Key=�6&   ��5_      �� $	 Pwd��$ BorderBrush�G� 5a      -   5         G�Name$	 Pwd��5a   &   $ 密码��5b      !
   St.Common.PasswordBoxHelper 
  Attach .  
  Password 5d      �� *
Password+$ TwoWay=�$ PropertyChanged=�{�5e   "   �� *IsLoginEnabled+5f      ]� 5h      -   5         ]�Name$ 	Dockpanel��5i      $��3��5h       .+�5k      �� $��记住密码��6#    d� 	IsChecked 5l      �� *
RememberMe+{�5m      �� *IsLoginEnabled+�� 5n      $��自动登录��6#   $��Right=�5o       5p      �� *	AutoLogin+{�5q      �� *IsLoginEnabled+5r      � 5t      $��4��6    � ColumnDefinitions 5u      �� 5v      �� 5w      $
��Auto �6/   �� 5x      .+�5{      �� 2  ,System.Windows.Markup.XmlAttributeProperties   XmlSpace  preserve5�      $��	登   录��5{   !   ! �� 	IsDefault! . $��1��5~      ��5      �� $ 	Dockpanel��$ ActualWidth���5�      �� *LoginCommand+{�5�      �� *IsLoginEnabled+� 5�      " � Value$" 0Q�6   $��2��5�      # � IsIndeterminate# 5�      �� *ShowProgressBar+5�      5�      � 5�      $��5��6   5�      5�      5�      '  !MaterialDesignThemes.Wpf.Snackbar  5�   
   $��Bottom=�5�   v   ��6   �� $ LoginV��$ ActualWidth�$   MessageQueue$ 6P   �� *MessageQueue+5�      5�      5�      