   M S B A M L   `   `   `  ����F  BYinyinpedia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YinyinpediaYinyinpedia  5   	       Yinyinpedia.cartBuyer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Yinyinpedia     5   	     � Title$  	cartBuyer��5   	   $	��400��6   $	��420��6(    � WindowStartupLocation$ CenterScreen=�64    � WindowStyle$
 None=�6Y   .��5	      � .+�5
   
   �� $��1q�6   $��20 30 20 30q�6%   ���	#FFDDDBDB���	#FFFCD1D16R   �� 5   
   $��30 50 30 25q�6   .+�5      �� $��Delivery Destination��6   $��Center=�63   $��20(�6P   $��0 0 0 10q�6^   �� 5      
���iii�$��1q�6+   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��
10 10 10 0q�6O   .+�5      �� $��	10 10 0 0q�6   .+�5      �� $
��Name��6   �� 5      $��Phone Number��6   �� 5      $��Address��6   �� 5      $
��City��6   �� 5      $��Delivery Service��6   $��0 5 0 0q�67   �� 5      $��Discount��6   �� 5      $��
Grandtotal��6   5      �� 5      $��	10 17 0 0q�6   .+�5      �� -   5        $
��name��5      $	��200��6*   
�������
�������6N   �� 5      -   5        $��phoneNumber��5      $	��200��61   $��0 5 0 0q�6=   
�������
�������6f   �� 5      -   5        $��address��5      $	��200��6-   $��0 7 0 0q�69   
�������
�������6b   �� 5      -   5        $
��city��5      $��0 7 0 0q�6+   
���������5      �� 5       �� EndPoint$	 0,1&�62    �� 
StartPoint$	 0,0&�6A   .��5   "   �  � Color$ 	#FFF0F0F0��6/    � Offset$ 0Q�6A   � 5    "   $ 	#FFB6AFAF��6/   $ 1Q�6A   5!      5#      �� 5$      -   5        $��shipping��5$      $��0 7 0 0q�6/   
���������5%      �� 5&      $	 0,1&�62   $	 0,0&�6A   .��5'   "   � $ 	#FFF0F0F0��6/   $ 0Q�6A   � 5(   "   $ 	#FFB6AFAF��6/   $ 1Q�6A   5)      5+      �� 5,      -   5        $��promoDelivery��5,       $��-��65   $��Right=�6>   $��0 7 0 0q�6Z   �� 5-      -   5        $��
grandtotal��5-       $��-��62   $��Right=�6;   $��0 10 0 0q�6W   5.      5/      �� 50      $ 
Horizontal=�6   $��0 15 0 0q�62   .+�51      �� -   5        $��topup��51      $��TOP UP��6&   $��35 0 0 0q�67   $��Right=�6J   $��80��6f   $��30��6q   ���	#FFFF7777���	#FFF1CECE6�   �� 52      -	   5        $
��done��52      $
��DONE��6%   $��29 0 0 0q�64   $��Right=�6G   $��80��6c   $��30��6n   ���	#FFFF7777���	#FFF1CECE6�   �� 53      -
   5        $��cancel��53      $��CANCEL��6'   $��30 0 0 0q�68   $��Right=�6K   $��80��6g   $��30��6r   ���	#FFFF7777���	#FFF1CECE6�   54      55      56      57      