   M S B A M L   `   `   `  ����E  AMapBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MapBuilder
MapBuilder  5   �   &     MapBuilder.RobotPositionsWindows   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   *localclr-namespace:MapBuilder     6�   -   5          � Title$9  3Информация о позоция робота��5   	   $	��300��6-   ���	#FFFBB54F$��661.539��6Q    � 	Resources 5         positionsViewSource��%   &	        �� 5   
   6   5      .��5	      �  � DataContext# [  6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ positionsDataGrid��5
         AutoGenerateColumns .     EnableRowVirtualization . $��10,10,306,71q�6�      RowDetailsVisibilityMode$ VisibleWhenSelected=�6�    �� SelectedIndex$ 0��6�      CanUserAddRows .  	   CanUserDeleteRows	 .  
   CanUserReorderColumns
 .     CanUserResizeColumns .     CanUserResizeRows .  ��6i   �� ��6�   Z� ��6�   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        coordXColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Координата Х��5   V      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6l   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *CoordX+6l     5      -   5        coordYColumn��$ Координата Y��5   V   $ * 6l    6;   �� *CoordY+6l     5      -   5        angleColumn��$ Угол поворота��5   T   $ * 6k    6:   �� *Angle+6k     5      -   5        iD_mapColumn��$ ID map��5   V   $ * 6f      
Visibility$ Hidden=�6p    6;   �� *ID_map+6p     5      -   5        iD_positionColumn��$ ID��5   `   $ SizeToHeader 6l    6@   �� *ID_position+6l   5      �� 5   
   $
��Left=�6   $	��189��60   $��
353,10,0,0q�6=   $	��Top=�6Q   $	��291��6i   ���	#FF8C8E94��6u   Z� .@�5      �� $#��Добавить запись��6   $��0q�6/   ���	#FF8C8E94��6:   Z� .��5      � $��0,0,0,0q�6   .+�5      �� -   5         ��Name$ 	txtCoordX��5      $
��Left=�61   $��23��6L   $��
151,10,0,0q�6X    �� TextWrapping$
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5      -	   5        $ 	txtCoordY��5      $
��Left=�61   $��23��6L   $��
151,44,0,0q�6X   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5      -
   5        $ txtAngle��5      $
��Left=�60   $��23��6K   $��
151,78,0,0q�6W   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��120��6�   �� 5      $��Координата Х:��6   $
��Left=�64   $��8,8,0,0q�6O   $	��Top=�6`   $	��141��6x   �� 5      $��Координата Y:��6   $
��Left=�64   $��8,41,0,0q�6O   $	��Top=�6a   $	��122��6y   �� 5      $ ��Угол поворота:��6   $
��Left=�65   $��8,74,0,0q�6P   $	��Top=�6b   $	��122��6z   �� 5      -   5         ��Name$ btnAdd��5      $��Добавить��6-   $
��Left=�6@   $��
10,110,0,0q�6[   $	��Top=�6o   $	��129��6�   $��39��6�    J� RenderTransformOrigin$ 0.503,-0.64&�6�   ��5      �� 5       �� EndPoint$	 0,1&�62    �� 
StartPoint$	 0,0&�6A   .��5   "   �  � Color$ 	#FFF3F3F3��6/    � Offset$ 0Q�6A   � 5    "   $ 	#FFEBEBEB��6/   $	 0.5Q�6A   � 5!   "   $ 	#FFDDDDDD��6/   $	 0.5Q�6A   � 5"   "   $ 	#FFFFD35F��6/   $ 1Q�6A   5#      5%      �� 5&      -   5        $ btnClear��5&      $��Очистить��6/   $
��Left=�6B   $��144,110,0,0q�6]   $	��Top=�6r   $	��127��6�   $��39��6�   ��5'      �� 5(      $	 0,1&�62   $	 0,0&�6A   .��5)   "   � $ 	#FFF3F3F3��6/   $ 0Q�6A   � 5*   "   $ 	#FFEBEBEB��6/   $	 0.5Q�6A   � 5+   "   $ 	#FFDDDDDD��6/   $	 0.5Q�6A   � 5,   "   $ 	#FFFFD35F��6/   $ 1Q�6A   5-      5/      50      51      �� 52      $!��Удалить запись��6   ���	#FF8C8E94��6F   Z� .��53      � .+�54      �� -   5        $ txtId��54      $
��Left=�6-   $��23��6H   $��	93,10,0,0q�6T   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��120��6�   �� 55      $��ID записи:��6   $
��Left=�61   $��10,7,0,0q�6L   $	��Top=�6^   �� 56      -   5        $ 	btnDelete��56      $��Удалить��60   $
��Left=�6B   $��	93,47,0,0q�6]   $	��Top=�6p   $	��120��6�   $��26��6�   ��57      �� 58      $	 0,1&�62   $	 0,0&�6A   .��59   "   � $ 	#FFF3F3F3��6/   $ 0Q�6A   � 5:   "   $ 	#FFEBEBEB��6/   $	 0.5Q�6A   � 5;   "   $ 	#FFDDDDDD��6/   $	 0.5Q�6A   � 5<   "   $ 	#FFFFD35F��6/   $ 1Q�6A   5=      5?      5@      5A      5B      5C      5D      