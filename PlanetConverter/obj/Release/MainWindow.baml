   M S B A M L   `   `   `  ���� J  FPlanetConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PlanetConverterPlanetConverter       PlanetConverter.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:PlanetConverter     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow��$
��1024��$	��768��
������� J� Opacity$ .9Q�$��0,0,0,0q�.��� -    �Name$ MainGrid��$��	.5,0,.5,0q� � ColumnDefinitions �� $��30 ��� $	��95* ��� $	��99* ��� $	��15* ��� $	��412 ��� $	��30* ��� $
��337* � � RowDefinitions �� $	��105 ��� $	��95* ��� $
��475* ��� $	��66* �.+�� $��0,0,1.143,5.143q�$��7��$��0������  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.���  � Offset$ 0Q� � Color$ 	#FF383838��� $ 0.869Q�$ Black��� $ 0.879Q�$ 	#FFC5C5C5��� $ 1Q�� $ 0.892Q�$ 	#BC5F5F5F��.+��� $	��190��$��57��$��90��$��
10,10,0,30q�$
��Left=�$��Center=�$0��*Assets/Amphenol-Antenna-Solutions-Logo.gif���� -   	 ��Name$	 ConversionCheckBoxes��$��1��$
��Left=�$	��411��$��12.286,24,0,0q�$��2��$	��Top=�$	��100��$��2��.+��� -   
 ��Name$
 SelectAllFormatsCheckBox�� d� IsThreeState .  $��
Select All��
���   � d� 	IsChecked$ FalseY��� $��05���� -   $
 AircomCheckBox��$��AIRCOM��
���   �$ FalseY��� $��05���� -   $
 AtollCheckBox��$��ATOLL��
���   �$ FalseY��� $��10���� -   $
 Ce4CheckBox��$	��CE4��
���   �$ FalseY��� $��10���� -   $
 CelCadCheckBox��$��CELCAD��
���   �$ FalseY��� $��10���� -   $
 CelPlanCheckBox��$��CELPLAN��
���   �$ FalseY��� $��10���� -	   $
 GeoplanCheckBox��$��GEOPLAN��
���   �$ FalseY��� $��10���� -
   $
 GranetCheckBox��$��GRANET��
���   �$ FalseY��� $��10���� -   $
 HodiaxCheckBox��$��HODIAX��
���   �$ FalseY��� $��10���� -   $
 HydraCheckBox��$��HYDRA��
���   �$ FalseY��� $��10���� -   $
 LccNetCheckBox��$��LCC-NET��
���   �$ FalseY��� $��10���� -   $
 NetplanCheckBox��$��NETPLAN��
���   �$ FalseY��� $��10���� -   $
 OdesseyCheckBox��$��ODESSEY��
���   �$ FalseY��� $��10���� -   $
 PathLossCheckBox��$��PATHLOSS��
���   �$ FalseY��� $��10���� -   $
 WizardCheckBox��$��WIZARD��
���   �$ FalseY��� $��4��$��2����. $��4.714,0,9.286,0.714q�.���� -   $	 FormVariablesOne��$��2��$
��Left=�$	��459��$��2��$	��Top=�$	��360��$��2��.+���  �� Orientation$ 
Horizontal=�.+��� $��Comments  : ��
���   ��� -    ��Name$ CommentsTextBox��$��23�� �� TextWrapping$
 Wrap=�$�� ��$	��270���� .��;9AIRCOM: -Frequency -Hwidth -Vwidth -FrontToBack #src=.pln�� $ 
Horizontal=�.+��� $��Max Gain  :         ��
���   ��� -   $ MaxGainTextBox��$��23��$
 Wrap=�$�� ��$	��252���� $��5���� $ 
Horizontal=�.+��� $��Polarization  ��
���   ��� -   $ PolarizationTextBox��$��23��$
 Wrap=�$�� ��$	��272���� $��5���� $��15���� .��1/ATOLL: -MaxGain -PatPostDate #Fam=Class=AntType�� $ 
Horizontal=�.+��� $��Beamwidth     :  ��
���   ��� -   $ BeamwidthTextBox��$��23��$
 Wrap=�$�� ��$	��255���� $ 
Horizontal=�.+��� $��Date :  ��
���   ��� -   $ DateTextBox��$��23��$
 Wrap=�$�� ��$	��303���� $��5���� $ 
Horizontal=�.+��� $��Dimensions  :  ��
���   ��� -   $ DimensionsTextBox��$��23��$
 Wrap=�$�� ��$	��262���� $��5���� $ 
Horizontal=�.+��� $��	Family : ��
���   ��� -   $ FamilyTextBox��$��23��$
 Wrap=�$�� ��$	��297���� $��5���� $ 
Horizontal=�.+��� $��Max Frequency  :         ��
���   ��� -   $ MaxFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��221���� $��5���� $ 
Horizontal=�.+��� $ ��Min Frequency   :         ��
���   ��� -   $ MinFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��220���� $��5���� $��5���� $ 
Horizontal=�.+��� $��Min Gain   :         ��
���   ��� -   $ MinGainTextBox��$��23��$
 Wrap=�$�� ��$	��251���� $��5���� $ 
Horizontal=�.+��� -   $" PatternElectricalTiltTextBox��$��23��$
 Wrap=�$�� ��$	��195���� $��5���� $ 
Horizontal=�.+��� $��Weight  :         ��
���   ��� -   $ WeightTextBox��$��23��$
 Wrap=�$�� ��$	��264���� $��15���� .��31CE4: -MaxGain -Date -MinFrq -MaxFrq -Polarization�� $ 
Horizontal=�.+��� $��Description  : ��
���   ��� -   $ DescriptionTextBox��$��23��$
 Wrap=�$�� ��$	��269���� $��15���� .��CELLCAD: #src=.pln -Date�� $ 
Horizontal=�.+��� $��Gain : ��
���   ��� -    $ GainTextBox��$��23��$
 Wrap=�$�� ��$	��307���� $��5���� $ 
Horizontal=�.+��� $��Time :         ��
���   ��� -!   $ TimeTextBox��$��23��$
 Wrap=�$�� ��$	��280���� $��15���� .��A?CELLPLAN: -MaxGain -Description -Hwidth -Vwidth -MinFrq -MaxFrq�� $ 
Horizontal=�.+��� $��Size  :         ��
���   ��� -"   $ SizeTextBox��$��23��$
 Wrap=�$�� ��$	��282���� $��5���� .��FDGEOPLAN: -MaxGain -Description -AntType=Family -Polarization -Weight�� $ 
Horizontal=�.+��� $(��"Azimuth Display Offset Deg     :  ��
���   ��� -#   $ AzimuthDisplayTextBox��$��23��$
 Wrap=�$�� ��$	��168���� $��5���� $ 
Horizontal=�.+��� $��Date Measured :  ��
���   ��� -$   $ DateMesuredTextBox��$��23��$
 Wrap=�$�� ��$	��247���� $��5���� $ 
Horizontal=�.+��� $��Depth : ��
���   ��� -%   $ DepthTextBox��$��23��$
 Wrap=�$�� ��$	��298���� $��5���� $ 
Horizontal=�.+��� $��Electrical Tilt : ��
���   ��� -&   $ ElectricalTiltTextBox��$��23��$
 Wrap=�$�� ��$	��265���� $ 
Horizontal=�.+��� $��Height  :         ��
���   ��� -'   $ HeightTextBox��$��23��$
 Wrap=�$�� ��$	��266���� $��5���� $ 
Horizontal=�.+��� $ ��Lower Frequency :         ��
���   ��� -(   $ LowerFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��215���� $��5���� $ 
Horizontal=�.+��� $!��Upper Frequency  :         ��
���   ��� -)   $ UpperFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��211���� $��5���� $ 
Horizontal=�.+��� $��Width  :         ��
���   ��� -*   $ WidthTextBox��$��23��$
 Wrap=�$�� ��$	��270���� $��15���� .��><GRANET: -MaxGain -vbwidth -vbWith -FrontToBack -Polarization�� $ 
Horizontal=�.+��� $��Lobe Tilt :         ��
���   ��� -+   $ LobeTiltTextBox��$��23��$
 Wrap=�$�� ��$	��259���� $��5���� $ 
Horizontal=�.+��� $��Wind Area  :         ��
���   ��� -,   $ WindAreaTextBox��$��23��$
 Wrap=�$�� ��$	��246���� $��5���� $ 
Horizontal=�.+��� $!��Meas Frequency   :         ��
���   ��� --   $ MeasFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��213���� $��15���� .��	Hodiax:�� $��15���� .��1/Hydra: -Gain -Description -hwidth -vwidth -Date�� .��(&-width -depth -frequency -polarization�� $ 
Horizontal=�.+��� $��Length   :         ��
���   ��� -.   $ LengthTextBox��$��23��$
 Wrap=�$�� ��$	��262���� $��5���� $ 
Horizontal=�.+��� $��Low Frequency  :         ��
���   ��� -/   $ LowFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��222���� $��5���� $ 
Horizontal=�.+��� $!��High Frequency   :         ��
���   ��� -0   $ HighFrequencyTextBox��$��23��$
 Wrap=�$�� ��$	��215���� $��5���� $ 
Horizontal=�.+��� $��Max Power  :         ��
���   ��� -1   $ MaxPowerTextBox��$��23��$
 Wrap=�$�� ��$	��244���� $��15���� .��86LCC: -MaxGain -MinFreq -hbwidth -vbwidth -MaxFrequency�� .��-dimensions -length�� $��15���� .��
NETPLAN:�� $��15���� .��42ODYSSEY: -MaxGain -Gain -Frequency -ElectricalTilt�� $��5���� .��CAPATHLOSS: -Gain -Description -LowFrequency -Date -Max Pow -length�� .��-width -frequency�� $ 
Horizontal=�.+��� $��ATVSWR  :  ��
���   ��� -2   $ AtvswrTextBox��$��23��$
 Wrap=�$�� ��$	��278���� $��15���� .��DBWIZARD: -maxgain -mingain -description -length -date -polarization�� .��-hbeam -vbeam�� $ 
Horizontal=�.+��� $��FCC ID  :  ��
���   ��� -3   $ FccIdTextBox��$��23��$
 Wrap=�$�� ��$	��289���� $��5���� $ 
Horizontal=�.+��� $��MFR ID   :         ��
���   ��� -4   $ MfrIdTextBox��$��23��$
 Wrap=�$�� ��$	��261���� $��15��� -5   $ DisplayGrid��$��6��$��2��$��7.429,0,25.143,0.714q� �� $��* ��� $��* ��� $��* �.+��� ��. $��0,0,0,0q�.���� -6   $ SuccessResults��$��Stretch=�$��Stretch=�$
 Wrap=�$�� ��$	��Top=�$
��Auto��$
��Auto���� $��1��$��1���� ��. $��2��$��0,0,0,0q�.���� -7   $ ErrorResults��$��Stretch=�$��Stretch=�$
 Wrap=�$�� ��$	��Top=�$
��Auto��$
��Auto���� $��1��$��1���� $ 
Horizontal=�$��4��$��3.286,105,6.286,55q�$��2��$��1��.+��� -8    ��Name$ BrowseDirSrcButton��$��
Source Dir��$��5q�$��25��$��65���� -9   $ TargetDirectory��$��
Target Dir��$��5q�$��25��$��65��
�������� -:   $ ConvertDownloadButton��$��Download��$��5q�$��25��$��65�����#1589FF�� -;   $ Reset��$��Reset��$��5q�$��25��$��65��
��������� -<   $ PopulateButton��$��Populate��$��5q�$��25��$��65��
��������� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,100,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=� J� RenderTransformOrigin$ 	0.5,1.842&�.+�	� -=    	�Name$ Ce4VwaRadioButton��$
��.vwa��$��2,0,2,0q�$��Center=� 	� 	GroupName$ Ce4RadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� ->   $ Ce4TxtRadioButton��$
��.txt��$��2q�$��Center=�$ Ce4RadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,185,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -?   $ GeoPlanVwaRadioButton��$
��.vwa��$��2,0,2,0q�$��Center=�$ GeoPlanRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -@   $ GeoPlanTxtRadioButton��$
��.txt��$��2q�$��Center=�$ GeoPlanRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,213.7,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -A   $ GranetPatRadioButton��$
��.pat��$��2,0,1,0q�$��Center=�$ GranetRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -B   $ GranetTxtRadioButton��$
��.txt��$��2q�$��Center=�$ GranetRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,242,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -C   $ HodiaxHodRadioButton��$
��.hod��$��2q�$��Center=�$ HodiaxRadioButtons��$
 TrueY�$
��Left=�$��Center=�	� -D   $ HodiaxTxtRadioButton��$
��.txt��$��2q�$��Center=�$ HodiaxRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,271,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -E   $ HydraAdfRadioButton��$
��.adf��$��2,0,4,0q�$��Center=�$ HydraRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -F   $ HydraTxtRadioButton��$
��.txt��$��2q�$��Center=�$ HydraRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,300,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -G   $ LccAntRadioButton��$
��.ant��$��2,0,4,0q�$��Center=�$ LccRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -H   $ LccTxtRadioButton��$
��.txt��$��2q�$��Center=�$ LccRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,390.3,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -I   $ PathLossAdfRadioButton��$
��.adf��$��2,0,4,0q�$��Center=�$ PathLossRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -J   $ PathLossTxtRadioButton��$
��.txt��$��2q�$��Center=�$ PathLossRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� $��2��$ Vertical=�$
��Left=�$��20��$��0.429,418.9,0,0q�$��2��$	��Top=�$��89��.+��� $ 
Horizontal=�.+�	� -K   $ WizardApfRadioButton��$
��.apf��$��2,0,4,0q�$��Center=�$ WizardRadioButtons��$
 TrueY�$��41��$
��Left=�$��Center=�	� -L   $ WizardTxtRadioButton��$
��.txt��$��2q�$��Center=�$ WizardRadioButtons��$ FalseY�$��41��$
��Left=�$��Center=��� -M    ��Name$ 
DebugLabel��$��Debug Label :��$��4��$
��Left=�$��3.714,25.286,-4.857,0q�$��3��$	��Top=�$��3��$	��780��$��26���� -N   $ ResultsLabel��$�� ��$��4��$��14(�$��SemiBold"�$��29��$��Right=�$��0,34.857,25.143,0q�$��1��$	��Top=�$	��643��$��3��