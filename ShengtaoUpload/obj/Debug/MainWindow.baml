   M S B A M L   `   `   `  ����D  @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common  5       	St.Upload   St.Upload.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      7common*clr-namespace:St.Common;assembly=St.Common  5      U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  5	      $��15(�5      $	��650��5        � 
ResizeMode$  NoResize=�5      $	��900��5       � Title$ 资源列表��5       � Topmost .  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+6F   5       � 	Resources 5      %   St.Common.DateToStringConverter   DateToStringConverter%   St.Common.BoolToStringConverter  BoolToStringConverter#   St.Common.LongStringConverter  LongStringConverter$   St.Common.EnableOrNotConverter  EnableOrNotConverter,	   &St.Common.ReUploadEnableOrNotConverter   ReUploadEnableOrNotConverter�� 5   
   %�   &	        &	      &	 &     &	 9     &	 L       5      6K     5       6K     5!      6G     5"      6I   	  5#      6Y   5$      .��5'      � -   5         �Name$ ResourceList��5'      	 � RowDefinitions	 5(   
   �� 5)      $��50 �6   �� 5*      �� 5+      $��40 �6   
 � ColumnDefinitions
 5-   
   �� 5.      $��20 �6   6,   �� 5/      6!   �� 50      $��20 �6   6,   .+�52   
   ]� $��0��6   $��1��6!   $
��Left=�61   .+�53      �� $��搜索��6    �� 	IsDefault . ��63   �� *QueryCommand+6V   �� 54      -   5         ��Name$ 
SourceName��54      )
  #MaterialDesignThemes.Wpf.HintAssist 
  Hint$ 资源名称��6*   $	��150��6D   ��6P   �� *
SourceName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   55      ]� 57   
   $��0��6   $��1��6!   $��Right=�61   .+�58      �� -   5        $��
BtnRefresh��58      $��Right=�6'   
������ 
������  � ToolTip$ 刷新��6w   ��6�   �� *RefreshCommand+.��59      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind$ Refresh=�6   6/   5:      5;      � 5=   
   $��1��6   $��1��6     MaterialDesignBackground#��[ 6,   $��Stretch=�6c   .+�5>      &   System.Windows.Controls.DataGrid  -   5          Name$ GridUploadFiles��5>         CanUserAddRows .     CanUserSortColumns . $��15(�5A         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5B         AutoGenerateColumns .  ��5D      �� *ResourceList+ �� SelectedItem 5E      �� *SelectedResource+ 5I      *  $System.Windows.Controls.DataGridCell �� 
TargetType  MaterialDesignDataGridCell �� BasedOn �� Property �� Value��%_   '        7  �� 5J      "  6   8   .�5K      ��  � ��$ 0q�6<   6H   5L      5M         Columns 5N      4  .System.Windows.Controls.DataGridTemplateColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header 5Q      �� 5R      -   5         ��Name$ Id��5R   %   $��序号��62   
������ 
������ 6w      CellTemplate 5T      �� 5U      .R�5V   "   �� $��Center=�6,   $��50��6[   ��6G   �� *Id+6h   5W      5Y        5[       5\      �� 5]      -   5        $
 Name��5]   %   $��资源名称��63   
������ 
������ 6�    5_      �� 5`      .R�5a   "   �� $��Center=�6,   $��60��6�   $	��120��6�   ��6G   �� *Name+  �� 	Converter#  [  6�   �� *Name+6�   5b      5d        5f       5g      �� 5h      -   5        $ UploadStatus��5h   %   $��是否已上传��6;   
������ 
������ 6�    5j      �� 5k      .R�5l   "   �� $��Center=�6,   $��50��6�   ��6G   �� *UploadStatus+#  [ 6�   5m      5o        5q       5r      �� 5s      -   5        $ AddTime��5s   %   $��添加时间��66   
������ 
������ 6�    5u      �� 5v      .R�5w   "   �� $��Center=�6,   $	��150��6�   ��6G   �� *	AddTime+#  [  6�   5x      5z        5|       5}      �� 5~      .R�5   "   �� $��上传��6)   $��80��6�   ��66   �� *
GotoUpload+{�6U   �� *UploadStatus+#  [ 6�   5�      5�        5�       5�      �� 5�      .R�5�   "   �� $��重传��6)   $��80��6�   ��66   �� *ReGotoUpload+{�6W   �� *UploadStatus+#  [ 6�   5�      5�        5�       5�      �� 5�      .R�5�   "   �� $��详情��6)   $��80��6U   ��66   �� *
GotoDetail+6b   5�      5�      5�      5�      � 5�   
   $��2��6   $��1��6   
 5�      �� 5�      $
��278* �6#   62   �� 5�      $
��178* �6#   62   �� 5�      $	��67* �6#   61   �� 5�      $
��138* �6#   62   �� 5�      $
��143* �6#   62   .+�5�      �� $��0��6   $	��100��6%   $��Right=�61   $
��Auto��6M   $	��0,4q�6[   ��6h   �� *PrePageCommand+.��6�   	上一页6�   �� 5�      $��1��6   $	��100��6&   $
��Left=�62   $
��Auto��6M   $	��5,4q�6[   ��6h   �� *NextPageCommand+.��6�   	下一页6�   �� 5�      $��2��6   $��50��6'   $
��Left=�62   ! �� TextAlignment$! Center=�6M   ��6d   �� *	PageIndex+$ TwoWay=�$ PropertyChanged=�6�   �� 5�      $��3��6   $	��100��6&   $
��Left=�62   $
��Auto��6M   $	��0,4q�6[   ��6h   �� *GoToPageCommand+.��6�   Go6�   ]� 5�      $��4��6   $��Center=�6)   $��Center=�6F   $��60��6a   .+�5�      �� $��Center=�6   $��Center=�65   .��6P   共6S   �� 5�      $��Center=�6   $��Center=�69   ��6T   �� *	TotalPage+$ OneWay=�$ PropertyChanged=�6�   �� 5�      $��Center=�6   $��Center=�65   .��6P   页6S   5�      5�      5�      5�      