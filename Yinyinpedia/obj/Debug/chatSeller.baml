   M S B A M L   `   `   `  ����F  BYinyinpedia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YinyinpediaYinyinpedia  5   	       Yinyinpedia.chatSeller   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Yinyinpedia     5   	     � Title$  
chatSeller��5   	   $	��530��6   $	��650��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$
 None=�6Z   .��5	      � .+�5
   
   �� $��1q�6   $��20 30 20 30q�6%   ���	#FFDDDBDB���	#FFFCD1D16R   �� 5   
   $��30 50 30 25q�6   .+�5      �� $
��Chat��6   $��Center=�6#   $��20(�6@   $��0 0 0 10q�6N   �� 5      
���iii�$��1q�6+   �� 5      -   5        $
��back��5      $
��Back��6!   $��Right=�6C   $��30��6_   $��80��6k   $��
0 10 20 10q�6v   ���	#FFFF7777���	#FFF1CECE6�   �� 5      
���iii�$��1q�6+   �� 5       �� Orientation$ 
Horizontal=�6   $��
10 10 10 0q�62   .+�5      �� $��
10 10 0 10q�6   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dgvChat��5      $	��204��6.   $	��260��6:   
�������
�������   
IsReadOnly . 6�   �� 5      -   5        $��delete��5      $��
Clear Chat��6+   $��80��6@   $��0 10 0 0q�6K   $��30��6]   ���	#FFFF7777���	#FFF1CECE6�   5      �� 5      $��
10 10 10 0q�6   .+�5      �� -   5        $��listChat��5      
�������
�������$	��340��6]   $	��260��6i   ��5      �� 5       �� 
TargetType" ��6$   .�5   "   ��  �� Property � �� �� Value$ Stretch=�6O   5      5      �� 5      $ 
Horizontal=�6!   $��10 10 10 10q�6:   .+�5      �� $��Reply��6    $��30��60   �� 5      -   5        $��reply��5   "   $	��180��6/   $��20��6;   $��10 0 0 0q�6G   
�������
�������6q   �� 5       -   5        $��submit��5    !   $
��Send��6/   $��80��6>   $��10 0 0 0q�6I   $��30��6[   ���	#FFFF7777���	#FFF1CECE6�   5!      5"      5#      5$      5%      5&      