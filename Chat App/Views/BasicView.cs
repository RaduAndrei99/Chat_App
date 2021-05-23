using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App.Views
{
    /// <summary>
    /// Clasă ce reprezintă un view.
    /// Moștenită de celalte view-uri.
    /// </summary>
    public partial class BasicView : MaterialForm
    {
        /// <summary>
        /// Referință către managerul framework-ului materialskin.
        /// Se ocupă de culorile și tema aplicației.
        /// </summary>
        protected readonly MaterialSkinManager _manager;
        
        /// <summary>
        /// Constructor.
        /// Setează o temă implicită.
        /// </summary>
        public BasicView()
        {
            InitializeComponent();
            _manager = MaterialSkin.MaterialSkinManager.Instance;
            _manager.AddFormToManage(this);
            _manager.Theme = MaterialSkinManager.Themes.DARK;
            _manager.ColorScheme = new ColorScheme(Primary.DeepOrange300, Primary.DeepOrange500, Primary.DeepOrange500, Accent.DeepOrange700, TextShade.BLACK);
        }
    }
}
