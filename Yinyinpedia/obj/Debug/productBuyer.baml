   M S B A M L   `   `   `  ����F  BYinyinpedia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YinyinpediaYinyinpedia  5   	       Yinyinpedia.productBuyer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Yinyinpedia     5   	     � Title$  productBuyer��5   	   $	��700��6   $
��1020��6+    � WindowStartupLocation$ CenterScreen=�68    � WindowStyle$
 None=�6]   .��5	      � .+�5
   
   �� $��1q�6   $��20 30 20 10q�6%   ���	#FFDDDBDB���	#FFFCD1D16R   �� 5   
   $��25 50 25 25q�6   .+�5      �� $��Products��6   $��Center=�6'   $��20(�6D   $��0 0 0 10q�6R   �� 5      
���iii�$��1q�6+   �� 5      -   5        $
��back��5      $
��Back��6!   $��Right=�6C   $��30��6_   $��80��6k   $��
0 10 35 10q�6v   ���	#FFFF7777���	#FFF1CECE6�   �� 5      
���iii�$��1q�6+   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0 10 0 0q�6O   .+�5      �� $��10 0 0 0q�6   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $��Search��6    $��0 0 10 0q�61   �� 5      -   5        $��keyword��5   "   $	��350��61   
�������
�������6U   5      �� 5      $ 
Horizontal=�6!   $��Center=�6:   $��0 5 0 0q�6W   .+�5      �� $��Rating��6    $��0 0 10 0q�61   �� 5      -   5        $��rating��5   #   $	��125��61   
���������5      �� 5   "    �� EndPoint$	 0,1&�66    �� 
StartPoint$	 0,0&�6E   .��5   &   �  � Color$ 	#FFF0F0F0��63    � Offset$ 0Q�6E   � 5   &   $ 	#FFB6AFAF��63   $ 1Q�6E   5   #   .@�5      �� $��	Ascending��6+   �� 5       $��	Decending��6+   5!      �� 5"      $��	Condition��6    $��	10 0 10 0q�64   �� 5#      -   5        $
��city��5#   #   $	��125��6/   
���������5$      �� 5%   "   $	 0,1&�66   $	 0,0&�6E   .��5&   &   � $ 	#FFF0F0F0��63   $ 0Q�6E   � 5'   &   $ 	#FFB6AFAF��63   $ 1Q�6E   5(   #   .@�5*      �� $	��New��6+   �� 5+      $
��Used��6+   5,      5-      5.      �� 5/      $ 
Horizontal=�6   $��20 0 0 0q�67   .+�50      �� -   5        $��search��50      $��Search��6+   $	��Top=�6Q   $��Center=�6i   $��30��6�   $��80��6�   ���	#FFFF7777���	#FFF1CECE6�   �� 51      -   5        $��refresh��51      $��Refresh��6,   $��10 0 0 0q�6T   $	��Top=�6f   $��Center=�6~   $��30��6�   $��80��6�   ���	#FFFF7777���	#FFF1CECE6�   52      53      �� 54      $ 
Horizontal=�6   $��0 20 0 0q�62   .+�55      �� $��10 0 0 0q�6   .+�56      �� $��Product��6   $��Center=�6.   &   System.Windows.Controls.DataGrid  57      -   5        $��
dgvProduct��57      $	��408��61   $	��320��6=   
�������
�������   
IsReadOnly . 6�   �� 58      $ 
Horizontal=�6!   $��0 10 0 0q�6:   $��Right=�6L   .+�59      �� -   5        $
��prev��59   !   $
��Prev��6.   $��10 0 0 0q�6P   $��50��6b   $��30��6m   ���	#FFFF7777���	#FFF1CECE6�   �� 5:      -	   5        $
��page��5:   $   $��1��60   $��	10 2 10 0q�69   $��18(�6L   �� 5;      -
   5        $
��next��5;   !   $
��Next��6-   $��50��6O   $��30��6Z   ���	#FFFF7777���	#FFF1CECE6}   5<      5=      �� 5>      $��
10 110 0 0q�6   .+�5?      �� $��Amount Purchased��6   �� 5@      -   5        $��amount��5@      
�������
�������6D   �� 5A      -   5        $	��add��5A      $	��Add��6(   $��Center=�6H   $��0 10 0 0q�6e   $��30��6w   $��80��6�   ���	#FFFF7777���	#FFF1CECE6�   �� 5B      -   5        $��tanya��5B      
�������
�������$��0 10 0 0q�6[   $	��110��6m   �� 5C      -   5        $
��chat��5C      $
��Chat��6)   $��Center=�6K   $��0 10 0 0q�6h   $��30��6z   $��80��6�   ���	#FFFF7777���	#FFF1CECE6�   5D      �� 5E      $��10 0 0 0q�6   .+�5F      �� $
��Cart��6   $��Center=�6+     5G      -   5        $��dgvCart��5G      $	��408��6.   $	��320��6:   
�������
������� . 6�   �� 5H      $ 
Horizontal=�6!   $��0 5 0 0q�6:   $��Center=�6K   .+�5I      �� .+�5J      �� -   5        $
��bnyk��5J   (   $��Total Product :��64   �� 5K      -   5        $��bayar��5K   (   $��Total Price      :��65   5L      �� 5M      $��5 0 0 0q�6%   .+�5N      �� -   5        $��products��5N   (   $��0��68   �� 5O      -   5        $��price��5O   (   $��0��65   5P      5Q      �� 5R      -   5        $��submit��5R      {�.  $��Buy It !��6=   $��Center=�6e   $��0 10 0 0q�6�   $��30��6�   $��80��6�   ���	#FFFF7777���	#FFF1CECE6�   5S      5T      �� 5U      $ 
Horizontal=�6   $��Center=�62   $��	30 15 0 0q�6O   .+�5V      �� $��
Product : ��6   �� 5W      -   5        $��produk��5W      $��0��6*   5X      5Y      5Z      5[      