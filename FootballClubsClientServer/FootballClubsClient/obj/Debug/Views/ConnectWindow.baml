   M S B A M L   `   `   `  ����  FootballClubsClient-    'FootballClubsClient.Views.ConnectWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��
wndConnect��5   \      WindowBackground#��[  5   	     � Title$  ConnectWindow��5   	   $	��150��6   $	��300��6,    � WindowStartupLocation$ CenterOwner=�68    � WindowStyle$
 None=�6n    � 
ResizeMode$ NoResize=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $��5* �6   �� 5	      $��4* �6   �� 5
      $��6* �6    � ColumnDefinitions 5   
   �� 5      $	��35* �6   �� 5      $	��38* �6   .+�5   
   �� -   5         ��Name$ tbPort��5      $
��Left=�6"   $��Segoe UI Light)�6p   $��1��6�   $��0��6�   $��25��6�   $��16,5,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6=   ��  �� ElementName$ 
wndConnect��	 �� Path$
	 Port�6�   �� 5   
   $��Введите порт:��6   $��Segoe UI Light)�6(   $��14(�6D   $��0��6R   $��0��6b   $��Right=�6o   $��0,5,0,0q�6�   $	��Top=�6�   $��32��6�   $��96��6�   �� 5   
   -   5        $
 tbIP��5      $
��Left=�6    $��Segoe UI Light)�6s   $��1��6�   $��1��6�   $��23��6�   $��16,5,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6;   �� $ 
wndConnect��$	 	IpAddress�6�   �� 5   
   $��Введите IP:��6   $��0��6&   $��1��66   $��14(�6C   $��Segoe UI Light)�6Q   $��Right=�6m   $��0,5,0,0q�6�   $	��Top=�6�   $��26��6�   $��86��6�   �� 5   
   -   5        $��OK��5      $��1��6   $��20(�6.   $��Segoe UI Light)�6<   $��Center=�6X   $��2��6u   $��Center=�6�     SimpleButton#��[ 6�   $	��120��6�   $��40��6�   $��2,2,2,2q�6�   6�   5      5      