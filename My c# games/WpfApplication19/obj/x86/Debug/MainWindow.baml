   M S B A M L   `   `   `  ����K  GWpfApplication19, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfApplication19.UIWpfApplication19.UI  5   	   =#clr-namespace:WpfApplication19.DataWpfApplication19.Data  5   	   I)clr-namespace:WpfApplication19.ConvertersWpfApplication19.Converters  5   	   I)clr-namespace:WpfApplication19.PresentersWpfApplication19.Presenters  5   	   !    WpfApplication19.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   *ui!clr-namespace:WpfApplication19.UI  5   	   /model#clr-namespace:WpfApplication19.Data  5   	   :
converters)clr-namespace:WpfApplication19.Converters  5   	   5local)clr-namespace:WpfApplication19.Presenters  5   	     � Title$  
MainWindow��5	   	   $	��350��6   $	��525��6)    � 	Resources 5
      1   +WpfApplication19.Converters.ValuesConverter   valueConverter �� DataType$   WpfApplication19.Data.GameItem ��Name �� Mode �� 	Converter.  (System.Windows.Interactivity.Interaction   	Behaviors)   #WpfApplication19.UI.AdornerBehavior �� 
TargetType �� Triggers	 �� Binding
 �� Value �� Property �� Value%   WpfApplication19.Presenters.Row �� Orientation��%�  &	        (��      5      *, +)7   '�� �     (�� �    5.      *, +)  5   
   6%   �� 5   
   "  6   .R�5      �� $	 dor��5      $��50��5      $��50��5      ��5      �� *Value+$ TwoWay=�8   ��5      �� *setValueCommand+ 5        5      5      5      �� 5   
   " ��6    5      �� 5      	 6   �� *IsPartOfWinner+
 True6A   .a�5      ��  � ��
 �  ��63   5      �� 5      	 6   �� *	IsEnabled+	
 False6<   .a�5      ��  � {� .  62   5       5-      �� 5.   
   "  6   .R�5/      �� ��6   �� *Items+$ TwoWay=���50      �� 51      .R�52      �� $ 
Horizontal=�6%   53      55      56      57      .��58      �  � RowDefinitions 59   
   �� 5:      $��* �6   �� 5;      $
��auto �6   �� 5<      $
��auto �6   .+�5>   
   o� $��1��6   $��30��6   .@�5?      m� $��restart��6    m� Command 6(   �� *RestartGame+�� 5@      $��Rows and Columns Amount��6   �� 5A      $	��100��6S   ��6   �� *!TheGameManager.MatrixHeigtWidth+$ OneWay=�6S   5B      �� 5E   
   �� 5F      $��3��6   ��6#   �� *ArrayOfRows+$ TwoWay=�5G      5Q      5R      