   M S B A M L   `   `   `  ���� D  @St.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:St.Common;assembly=St.Common	St.Common   	St.Upload   St.Upload.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6mah,http://metro.mahapps.com/winfx/xaml/controls  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  7common*clr-namespace:St.Common;assembly=St.Common  U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes  $��15(�$	��650��  � 
ResizeMode$  NoResize=�$	��900�� � Title$ 资源列表�� � Topmost .  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+ � 	Resources %   St.Common.DateToStringConverter   DateToStringConverter%   St.Common.BoolToStringConverter  BoolToStringConverter#   St.Common.LongStringConverter  LongStringConverter$   St.Common.EnableOrNotConverter  EnableOrNotConverter,	   &St.Common.ReUploadEnableOrNotConverter   ReUploadEnableOrNotConverter�� %K   &	        &	      &	 
     &	      &	              	  .��� -    �Name$ ResourceList��	 � RowDefinitions	 �� $��50 ��� �� $��40 �
 � ColumnDefinitions
 �� $��20 ��� �� $��20 �.+�]� $��0��$��1��$
��Left=�.+��� $��搜索�� �� 	IsDefault . ���� *QueryCommand+�� -    ��Name$ 
SourceName��)
  #MaterialDesignThemes.Wpf.HintAssist 
  Hint$ 资源名称��$	��150������ *
SourceName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�]� $��0��$��1��$��Right=�.+��� -   $��
BtnRefresh��$��Right=�
������ 
������  � ToolTip$ 刷新������ *RefreshCommand+.��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind$ Refresh=�� $��1��$��1��  MaterialDesignBackground#��[ $��Stretch=�.+�&   System.Windows.Controls.DataGrid  -     Name$ GridUploadFiles��   CanUserAddRows .     CanUserSortColumns . $��15(�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    AutoGenerateColumns .  ���� *ResourceList+ �� SelectedItem �� *SelectedResource+ *  $System.Windows.Controls.DataGridCell �� 
TargetType  MaterialDesignDataGridCell �� BasedOn �� Property �� Value��%5   '        7  �� "  8   .���  � ��$ 0q�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header �� -    ��Name$ Id��$��序号��
������ 
������    CellTemplate �� .R��� $��Center=�$��50������ *Id+   �� -   $
 Name��$��资源名称��
������ 
������  �� .R��� $��Center=�$��60��$	��120������ *Name+  �� 	Converter#  [  �� *Name+   �� -   $ UploadStatus��$��是否已上传��
������ 
������  �� .R��� $��Center=�$��50������ *UploadStatus+#  [    �� -   $ AddTime��$��添加时间��
������ 
������  �� .R��� $��Center=�$	��150������ *	AddTime+#  [     �� .R��� $��上传��$��80������ *
GotoUpload+{��� *UploadStatus+#  [    �� .R��� $��重传��$��80������ *ReGotoUpload+{��� *UploadStatus+#  [    �� .R��� $��详情��$��80������ *
GotoDetail+� $��2��$��1��
 �� $
��278* ��� $
��178* ��� $	��67* ��� $
��138* ��� $
��143* �.+��� $��0��$	��100��$��Right=�$
��Auto��$	��0,4q����� *PrePageCommand+.��	上一页�� $��1��$	��100��$
��Left=�$
��Auto��$	��5,4q����� *NextPageCommand+.��	下一页�� $��2��$��50��$
��Left=�! �� TextAlignment$! Center=����� *	PageIndex+$ TwoWay=�$ PropertyChanged=��� $��3��$	��100��$
��Left=�$
��Auto��$	��0,4q����� *GoToPageCommand+.��Go]� $��4��$��Center=�$��Center=�$��60��.+��� $��Center=�$��Center=�.��共�� $��Center=�$��Center=����� *	TotalPage+$ OneWay=�$ PropertyChanged=��� $��Center=�$��Center=�.��页