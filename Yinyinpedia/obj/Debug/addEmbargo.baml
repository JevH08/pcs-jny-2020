   M S B A M L   `   `   `  ����F  BYinyinpedia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YinyinpediaYinyinpedia  5   	       Yinyinpedia.addEmbargo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Yinyinpedia     5   	     � Title$  
addEmbargo��5   	   $	��530��6   $	��550��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$
 None=�6Z   .��5	      � .+�5
   
   �� $��1q�6   $��20 30 20 20q�6%   ���	#FFDDDBDB���	#FFFCD1D16R   �� 5   
   $��30 50 30 25q�6   .+�5      �� $��Embargo��6   $��Center=�6&   $��20(�6C   $��0 0 0 10q�6Q   �� 5      
���iii�$��1q�6+   �� 5      -   5        $
��back��5      $
��Back��6!   $��Right=�6C   $��30��6_   $��80��6k   $��
0 10 20 10q�6v   ���	#FFFF7777���	#FFF1CECE6�   �� 5      
���iii�$��1q�6+   �� 5       �� Orientation$ 
Horizontal=�6   $��
10 10 10 0q�62   .+�5      �� $��
10 10 0 10q�6   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��
dgvEmbargo��5      $	��204��61   $	��240��6=   
�������
�������   
IsReadOnly . 6�   5      �� 5      $��	10 10 0 0q�6   .+�5      �� $
��Name��6   5      �� 5      $��10 15 10 10q�6   .+�5      �� -   5        $
��name��5      $	��180��6*   
�������
�������6N   �� 5      $ 
Horizontal=�6!   $��Right=�6:   $��0 10 0 0q�6V   .+�5      �� -   5        $��submit��5   !   $��Create��6/   $��80��6@   $��30��6K   ���	#FFFF7777���	#FFF1CECE6�   �� 5      -   5        $��delete��5   !   $��Delete��6/   $��10 0 0 0q�6@   $��80��6R   $��30��6]   ���	#FFFF7777���	#FFF1CECE6�   5      5      5      �� 5       $ 
Horizontal=�6   $��Center=�62   $��0 15 0 0q�6O   .+�5!      �� $��
Embargo : ��6   �� 5"      -   5        $��embargo��5"      $��0��6+   5#      5$      5%      5&      