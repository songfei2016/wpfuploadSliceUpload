   M S B A M L   `   `   `  ���� D  @St.Upload, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:St.Upload.ViewSt.Upload.View  D @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common #    St.Upload.View.ResourceUpload   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  (localclr-namespace:St.Upload.View  7common*clr-namespace:St.Common;assembly=St.Common  -   (  "MahApps.Metro.Controls.MetroWindow   Name$  
UploadView�� � 
ResizeMode$ NoResize=� � Topmost .  � Title$ 上传文件��$	��600��$	��850�� � 	Resources  �� 
TargetType �� Property �� Value   MaterialDesignTextBox �� BasedOn  MaterialDesignRaisedButton��%�  ']�       '�� :     '�� y     7   '� �     '��     7  �� " ]�.���  � ��$	 260����  � ��$ 10q��� " ��.���  � ��$ 10 5 5 5q���  � ��$ 20(��� " ��8   .���  � ��$ Center=���  � ��$ 10 5 5 5q���  � ��$ 20(��� " �.���  � ��$
 Left=���  � ��$ 20q��� " ��8   .���  � ��$ 10q���  � ��$ 20(���  � ��$ 40����  � ��$ Center=�.  (System.Windows.Interactivity.Interaction	   Triggers	 /  )System.Windows.Interactivity.EventTrigger  
   	EventName$
 Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��292* ��� $��5* �.+�]� $��0��$��0��$��22,10,21,10q�$
��Auto��.+�� $	��250������  �� ElementName$ 
UploadView�� �� Path$ ActualWidth� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��100 � �� $	��200 ��� $��* �.+�]� $��0��$��0��.+��� $��	资源名��]� $��0��$��1��$
��Left=�.+��� ���� *ReSourceName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�]� $��1��$��0��.+��� $��上传状态��]� $��1��$��1��$
��Left=�.+��� ���� *UploadStatus+]� $��2��$��0��.+��� $��	文件名��]� $��2��$��1��$
��Left=�.+��� ���� *
FileName+$ TwoWay=�$ PropertyChanged=�]� $��3��$��0��.+��� $��地址��]� $��3��$��1��$
��Left=�.+��� -    ��Name$ TextBox��$��0q� �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�$	��Top=����� *	FileUrl+$ TwoWay=�$ PropertyChanged=�]� $��1��$��0��$��22,10,21,10q�$
��Auto��.+�� $	��776��$	��200�� �� $
��Auto ��� $
��Auto ��� $
��Auto � �� $	��200 ��� $��* �.+�]� -    ]�Name$ 
Dockpanel1��$��0��$��0��$��
10,10,0,10q�$
��Auto��$
��Left=�.+��� 2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve-   $��BtnSelectFile��$��选择文件�� �� 	IsDefault . $
��Left=�$	��150������ *
SelectFile+]� -   $ 	Dockpanel��$��1��$��0��$
��Left=�$��
10,10,0,10q�$
��Auto��.+���  preserve-   $��	BtnUpload��$��上  传�� . $	��150������ *StartUpload+]� $��1��$��1��$
��Left=�$	��418��$
��0,10q�.+�� $	��247�� � Value �� *ProgressRate+$ TwoWay=�$ PropertyChanged=��� $��Center=�$��	0,22,0,24q�$	��131������ *ProgressRate+ �� StringFormat$
 {0}%��'  !MaterialDesignThemes.Wpf.Snackbar  $��2��$��0��$��2��$��Bottom=����� $ 
UploadView��$ ActualWidth�   MessageQueue �� *MessageQueue+