   M S B A M L   `   `   `  ����T  PImage_Gallery_Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Image_Gallery_ApplicationImage_Gallery_Application  5   	   *    $Image_Gallery_Application.EditWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Image_Gallery_Application     5   	     � Title$  
EditWindow��5   	   $	��750��6   $
��1500��6)    � WindowStartupLocation$ CenterScreen=�66   .��5	      � .+�5
   
   �� $	��750��6   $
��1500��6   $"��Images/editWindowLayout.jfif��6*   $
��Left=�6P   $��Center=�6k    �� Left$
 -128��6�   
 �� Top$
 -242��6�   $��UniformToFill=�6�   $��-2,0,0,0q�6�   �� 5   
   -   5         ��Name$ LoadingCanvas��5   <   $��50,150,271,65q�6   $
}�True��6(   .+�5      �� -   5         ��Name$	 img��5      $��Center=�5      $��Center=�6*   $��Uniform=�5   '   F� 5      -   5         F�Name$ selectionPath��5      
���� ��$��2��6;   5      �� 5   
    �� Orientation$ Vertical=�6   .+�5      �� $��Image Editing��6      	titleText#��[  5      
�������$��50(�5      $	��750��6*   $��0q�66    �� TextAlignment$ Center=�5      5      �� 5   
   -   5        $
��crop��5      $
��Crop��6   $
��Left=�6,   $��	50,73,0,0q�6G   $	��Top=�6Z     
mainButton#��[ 6r   6�   �� 5   
   -   5        	 ��Name$	 bw��5      $��B and W��6   $
��Left=�6/   $��
452,73,0,0q�6J   $	��Top=�6^   #��[ 6v   6�   �� 5   
   -   5        $		 red��5      $��Apply Red Mask��6   $
��Left=�67   $��
651,73,0,0q�6R   $	��Top=�6f   #��[ 6~   6�   �� 5   
   -   5        $	 green��5      $��Apply Green Mask��6    $
��Left=�6;   $��
854,73,0,0q�6V   $	��Top=�6j   #��[ 6�   6�   �� 5   
   -   5        $
	 blue��5      $��Apply Blue Mask��6   $
��Left=�69   $��1056,73,0,0q�6T   $	��Top=�6i   #��[ 6�   6�   �� 5    
   -	   5        $	 return��5       $��
Go to Main��6!   $
��Left=�66   $��1263,73,0,0q�6Q   $	��Top=�6f   #��[ 6~   6�   �� 5!   
   -
   5        $	 rotate��5!      $��Rotate��6!   $
��Left=�62   $��
248,73,0,0q�6M   $	��Top=�6a   #��[ 6y   6�   5"      5#      