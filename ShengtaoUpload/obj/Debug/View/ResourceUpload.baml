   M S B A M L   `   `   `  ����D  @St.Upload, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:St.Upload.ViewSt.Upload.View  5	   	   D @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common 5
   	   #    St.Upload.View.ResourceUpload   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   (localclr-namespace:St.Upload.View  5	   	   7common*clr-namespace:St.Common;assembly=St.Common  5
   	   -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  
UploadView��5   	    � 
ResizeMode$ NoResize=�5   	    � Topmost .  � Title$ 上传文件��5   	   $	��600��6   $	��850��6#    � 	Resources 5       �� 
TargetType �� Property �� Value   MaterialDesignTextBox �� BasedOn  MaterialDesignRaisedButton��%�  ']�       '�� m     '�� �     7   '� �    '�� �    7  �� 5   
   " ]�6   .�5      ��  � ��$	 260��6&   �� 5       � ��$ 10q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 10 5 5 5q�6'   �� 5       � ��$ 20(�6)   66   5      �� 5   
   " ��6   8   .�5      ��  � ��$ Center=�62   �� 5        � ��$ 10 5 5 5q�6'   �� 5!       � ��$ 20(�6)   66   5"      �� 5$   
   " �6   .�5%      ��  � ��$
 Left=�64   �� 5&       � ��$ 20q�6'   5'      �� 5)   
   " ��6   8   .�5*      ��  � ��$ 10q�6'   �� 5+       � ��$ 20(�6)   66   �� 5,       � ��$ 40��6'   64   �� 5-       � ��$ Center=�69   6J   5.      5/      .  (System.Windows.Interactivity.Interaction	   Triggers	 51      /  )System.Windows.Interactivity.EventTrigger  52   
   
   	EventName$
 Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 53      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+6F   54      .��57      �  � RowDefinitions 58   
   �� 59      $
��Auto �6   �� 5:      $
��Auto �6   �� 5;      $
��Auto �6    � ColumnDefinitions 5=   
   �� 5>      $
��292* �6   6.   �� 5?      $��5* �6   6,   .+�5C   
   ]� $��0��6   $��0��6!   $��22,10,21,10q�61   $
��Auto��6F   .+�5D      � $	��250��6M   ��6   ��  �� ElementName$ 
UploadView�� �� Path$ ActualWidth� 5E      �� 5F      $
��Auto �6$   �� 5G      $
��Auto �6$   �� 5H      $
��Auto �6$   �� 5I      $
��Auto �6$   �� 5J      $
��Auto �6$   �� 5K      $
��Auto �6$   �� 5L      $	��100 �6$    5O      �� 5P      $	��200 �6'   �� 5Q      $��* �6'   .+�5T      ]� $��0��6   $��0��6)   .+�5U      �� $��	资源名��6    6-   5V      ]� 5W      $��0��6   $��1��6)   $
��Left=�69   .+�5X      �� ��6    �� *ReSourceName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6r   5Y      ]� 5[      $��1��6   $��0��6)   .+�5\      �� $��上传状态��6    6.   5]      ]� 5^      $��1��6   $��1��6)   $
��Left=�69   .+�5_      �� ��6    �� *UploadStatus+6@   5`      ]� 5b      $��2��6   $��0��6)   .+�5c      �� $��	文件名��6    6-   5d      ]� 5e      $��2��6   $��1��6)   $
��Left=�69   .+�5f      �� ��6    �� *
FileName+$ TwoWay=�$ PropertyChanged=�6n   5g      ]� 5i      $��3��6   $��0��6)   .+�5j      �� $��地址��6    6,   5k      ]� 5l      $��3��6   $��1��6)   $
��Left=�69   .+�5m      �� -   5         ��Name$ TextBox��5m      $��0q�6/    �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�6L   $	��Top=�6�   ��6`   �� *	FileUrl+$ TwoWay=�$ PropertyChanged=�6�   5p      5q      5r      ]� 5t   
   $��1��6   $��0��6!   $��22,10,21,10q�61   $
��Auto��6F   .+�5u      � $	��776��6   $	��200��6    5v      �� 5w      $
��Auto �6$   �� 5x      $
��Auto �6$   �� 5y      $
��Auto �6$    5|      �� 5}      $	��200 �6'   �� 5~      $��* �6'   .+�5�      ]� -   5         ]�Name$ 
Dockpanel1��5�   9   $��0��6   $��0��6)   $��
10,10,0,10q�6M   $
��Auto��6a   $
��Left=�6o   .+�5�      �� 2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve6�   -   5        $��BtnSelectFile��5�   R   $��选择文件��6    �� 	IsDefault . $
��Left=�6g   $	��150��6�   ��6�   �� *
SelectFile+6�   5�      ]� 5�      -   5        $ 	Dockpanel��5�      $��1��5�      $��0��6)   $
��Left=�6:   $��
10,10,0,10q�5�   '   $
��Auto��6;   .+�5�      ��  preserve5�      -   5        $��	BtnUpload��5�      $��上  传��5�       . $	��150��5�      ��5�      �� *StartUpload+5�      5�      ]� 5�      $��1��6   $��1��6)   $
��Left=�69   $	��418��6U   $
��0,10q�6a   .+�5�      � $	��247��6r    � Value 6"   �� *ProgressRate+$ TwoWay=�$ PropertyChanged=�6r   �� 5�      $��Center=�6    $��	0,22,0,24q�6n   $	��131��6�   ��6;   �� *ProgressRate+ �� StringFormat$
 {0}%��6�   5�      5�      5�      '  !MaterialDesignThemes.Wpf.Snackbar  5�   
   $��2��6   $��0��6#   $��2��63   $��Bottom=�5�   n   ��6   �� $ 
UploadView��$ ActualWidth�   MessageQueue 6H   �� *MessageQueue+5�      5�      5�      