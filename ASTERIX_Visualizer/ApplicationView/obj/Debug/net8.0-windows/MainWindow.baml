   M S B A M L   `   `   `  ����J  FApplicationView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ApplicationViewApplicationView  5      O KMapManagementLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY@clr-namespace:MapManagementLibrary;assembly=MapManagementLibraryMapManagementLibrary 5      [ WSystem.Windows.Forms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5      ^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration 5	           ApplicationView.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /localclr-namespace:ApplicationView     5      Qmaps@clr-namespace:MapManagementLibrary;assembly=MapManagementLibrary     5      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5      _wfhOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration     5	        � Title$  
MainWindow��5      $	��450��6   $	��800��6&   .��5      � -   5         �Name$ mainGrid��5      .+�5   
   � -   5        $ selectFilesGrid��5      $��2��6:   $	��367��6N   $	��Top=�6[   $��	0,-22,0,0q�6s   .+�5      �� $��293,79,151,79q�6A   5      � 5   
   -   5        $ mapGridHost��5      $��110,10,10,10q�6$   .+�5      7  1System.Windows.Forms.Integration.WindowsFormsHost  -   5          Name$ 	formsHost��5      $��	2,10,0,10q�62   5      � 5   
   -   5        $ 	tableGrid��5      $
��Left=�6"   $	��186��6=   $��Center=�6J   $��	190,0,0,0q�6e   $��1��6x   $	��120��6�   .+�5      �� $
��Left=�6    �� TextWrapping$
 Wrap=�61   $��TextBox��6E   $	��Top=�6T   $	��120��6l   �� 5      $
��Left=�6   $��0,41,0,0q�61   $
 Wrap=�6C   $��TextBox��6W   $	��Top=�6f   $	��120��6~   �� 5      $
��Left=�6   $��0,96,0,0q�61   $
 Wrap=�6C   $��TextBox��6W   $	��Top=�6f   $	��120��6~   �� 5      $
��Left=�6   $��	0,168,0,0q�61   $
 Wrap=�6D   $��TextBox��6X   $	��Top=�6g   $	��120��6   5      � 5   
   -   5        $ grid3��5      $��Center=�6   $��Center=�6;   $��1��6h   $	��345��6x   $	��720��6�   .+�5      �� $
��Left=�6   $	��230��6/   $	��Top=�6<   $	��508��6T   $2��,/wp7412461-aesthetic-and-cute-wallpapers.jpg��6`   $��212,115,0,0q�6�   5      �� 5   
   -   5         ��Name$ selectFilesButton��5      $��Select Files��6,   $
��Left=�6C   $��50��6^   $��	10,66,0,0q�6j   $	��Top=�6}   $��81��6�   $��2��6�   �� 5   
   -   5        $ 	mapButton��5      $��View Map��6$   $
��Left=�67   $��50��6R   $��
10,134,0,0q�6^   $	��Top=�6r   $��81��6�   $��2��6�   �� 5   
   -	   5        $ tableButton��5      $��Table��6&   $
��Left=�66   $��50��6Q   $��
10,201,0,0q�6]   $	��Top=�6q   $��81��6�   $��2��6�   �� 5   
   -
   5        $ otherButton��5      $
��Left=�6&   $��50��6A   $��
10,270,0,0q�6M   $	��Top=�6a   $��81��6y   $��Other Button��6�   $��2��6�   �� 5    
   $��110,10,10,10q�6   
���   �$��1��69   $��2��6I   5!      5"      