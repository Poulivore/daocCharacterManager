using System;
using System.Windows.Data;

namespace daocCharacterManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NextRealmRankConverter : IValueConverter {
                public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)        
        {            
            int realmPoints = ( int )value;

	    return RealmPointsManager.GetNextRealmRank( realmPoints );
        }
        
        public object ConvertBack(object value, Type targetType, object parameter,
                    System.Globalization.CultureInfo culture) {
            return null; 
        }  
 
    }
}

