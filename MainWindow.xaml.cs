using EvidentaModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Proiect_Medii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        EvidentaEntitiesModel ctx = new EvidentaEntitiesModel();
        CollectionViewSource studentViewSource;
        CollectionViewSource facultateViewSource;
        CollectionViewSource catalogViewSource;
        CollectionViewSource studentCatalogsViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            studentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentViewSource")));
            facultateViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("facultateViewSource")));
            catalogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("catalogViewSource")));
            studentCatalogsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentCatalogsViewSource")));
            
            studentViewSource.Source = ctx.Students.Local;
            facultateViewSource.Source = ctx.Facultates.Local;
            catalogViewSource.Source = ctx.Catalogs.Local;

            ctx.Students.Load();
            ctx.Facultates.Load();
            ctx.Catalogs.Load();

            studentiCmbBox.ItemsSource = ctx.Students.Local;
            studentiCmbBox.SelectedValuePath = "studentId";

            facultateCmbBox.ItemsSource = ctx.Facultates.Local;
            facultateCmbBox.DisplayMemberPath = "numeFacultate";
            facultateCmbBox.SelectedValuePath = "facultateId";

            BindDataGrid();
        }

        private void btnSaveStudent_Click(object sender, RoutedEventArgs e)
        {
            
            Student student = null;
            if (action == ActionState.New)
            {
                try
                {
                    student = new Student()
                    {
                        nume = numeTextBox.Text.Trim(),
                        prenume = prenumeTextBox.Text.Trim(),
                        nrTel = nrTelTextBox.Text.Trim()
                    };
                    
                    ctx.Students.Add(student);
                    studentViewSource.View.Refresh();
                    
                    ctx.SaveChanges();  
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewStudent.IsEnabled = true;
                btnEditStudent.IsEnabled = true;
                btnDeleteStudent.IsEnabled = true;
                //btnSaveStudent.IsEnabled = false;
                //btnCancelStudent.IsEnabled = false;
                studentDataGrid.IsEnabled = true;
                btnPrevStudent.IsEnabled = true;
                btnNextStudent.IsEnabled = true;
                nrTelTextBox.IsEnabled = false;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;

            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    student = (Student)studentDataGrid.SelectedItem;
                    student.nume = numeTextBox.Text.Trim();
                    student.prenume = prenumeTextBox.Text.Trim();
                    student.nrTel = nrTelTextBox.Text.Trim();

                    ctx.SaveChanges(); 
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewStudent.IsEnabled = true;
                btnEditStudent.IsEnabled = true;
                btnDeleteStudent.IsEnabled = true;
                //btnSaveStudent.IsEnabled = false;
                //btnCancelStudent.IsEnabled = false;
                studentDataGrid.IsEnabled = true;
                btnPrevStudent.IsEnabled = true;
                btnNextStudent.IsEnabled = true;
                nrTelTextBox.IsEnabled = false;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
                
                studentViewSource.View.Refresh();
                studentViewSource.View.MoveCurrentTo(student);
            }

            else if (action == ActionState.Delete)
            {
                try
                {
                    student = (Student)studentDataGrid.SelectedItem;
                    ctx.Students.Remove(student);

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewStudent.IsEnabled = true;
                btnEditStudent.IsEnabled = true;
                btnDeleteStudent.IsEnabled = true;
                //btnSaveStudent.IsEnabled = false;
                //btnCancelStudent.IsEnabled = false;
                studentDataGrid.IsEnabled = true;
                btnPrevStudent.IsEnabled = true;
                btnNextStudent.IsEnabled = true;
                nrTelTextBox.IsEnabled = false;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;

                studentViewSource.View.Refresh();
            }
            SetValidationBindingStudent();
        }
        private void btnSaveFacultate_Click(object sender, RoutedEventArgs e)
        {
            Facultate facultate = null;
            if (action == ActionState.New)
            {
                try
                {
                    facultate = new Facultate()
                    {
                        numeFacultate = numeFacultateTextBox.Text.Trim(), 
                        nrTelFacultate = nrTelFacultateTextBox.Text.Trim()
                    };
                    ctx.Facultates.Add(facultate);
                    facultateViewSource.View.Refresh();

                    ctx.SaveChanges();

                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewFacultate.IsEnabled = true;
                btnEditFacultate.IsEnabled = true;
                btnDeleteFacultate.IsEnabled = true;
                //btnSaveFacultate.IsEnabled = false;
                //btnCancelFacultate.IsEnabled = false;
                facultateDataGrid.IsEnabled = true;
                btnPrevFacultate.IsEnabled = true;
                btnNextFacultate.IsEnabled = true;
                nrTelFacultateTextBox.IsEnabled = false;
                numeFacultateTextBox.IsEnabled = false;
                
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    facultate = (Facultate)facultateDataGrid.SelectedItem;
                    facultate.numeFacultate = numeFacultateTextBox.Text.Trim();
                    facultate.nrTelFacultate = nrTelFacultateTextBox.Text.Trim();

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewFacultate.IsEnabled = true;
                btnEditFacultate.IsEnabled = true;
                btnDeleteFacultate.IsEnabled = true;
                //btnSaveFacultate.IsEnabled = false;
                //btnCancelFacultate.IsEnabled = false;
                facultateDataGrid.IsEnabled = true;
                btnPrevFacultate.IsEnabled = true;
                btnNextFacultate.IsEnabled = true;
                nrTelFacultateTextBox.IsEnabled = false;
                numeFacultateTextBox.IsEnabled = false;

                facultateViewSource.View.Refresh();
                studentViewSource.View.MoveCurrentTo(facultate);
            }

            else if (action == ActionState.Delete)
            {
                try
                {
                    facultate = (Facultate)facultateDataGrid.SelectedItem;
                    ctx.Facultates.Remove(facultate);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewFacultate.IsEnabled = true;
                btnEditFacultate.IsEnabled = true;
                btnDeleteFacultate.IsEnabled = true;
                //btnSaveFacultate.IsEnabled = false;
                //btnCancelFacultate.IsEnabled = false;
                facultateDataGrid.IsEnabled = true;
                btnPrevFacultate.IsEnabled = true;
                btnNextFacultate.IsEnabled = true;
                //nrTelFacultateTextBox.IsEnabled = false;
                //numeFacultateTextBox.IsEnabled = false;
                
                facultateViewSource.View.Refresh();
            }
            SetValidationBindingFacultate();
        }
        private void btnSaveCatalog_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = null;
            if (action == ActionState.New)
            {
                try
                {
                    Student student = (Student)studentiCmbBox.SelectedItem;
                    Facultate facultate = (Facultate)facultateCmbBox.SelectedItem;

                    catalog = new Catalog()
                    {
                        facultateId = facultate.facultateId,
                        studentId = student.studentId
                    };
                    ctx.Catalogs.Add(catalog);
                    studentCatalogsViewSource.View.Refresh();

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewCatalog.IsEnabled = true;
                
                btnDeleteCatalog.IsEnabled = true;
                btnSaveCatalog.IsEnabled = false;
                btnCancelCatalog.IsEnabled = false;
                catalogDataGrid.IsEnabled = true;
                studentiCmbBox.IsEnabled = false;
                facultateCmbBox.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {

                try
                {
                    dynamic selectedOrder = catalogDataGrid.SelectedItem;
                    int curr_id = selectedOrder.catalogId;
                    var deletedOrder = ctx.Catalogs.FirstOrDefault(s => s.catalogId == curr_id);
                    if (deletedOrder != null)
                    {
                        ctx.Catalogs.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Eliminare executata cu succes", "Message");
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewCatalog.IsEnabled = true;
                btnDeleteCatalog.IsEnabled = true;
                btnSaveCatalog.IsEnabled = false;
                btnCancelCatalog.IsEnabled = false;
                catalogDataGrid.IsEnabled = true;
                studentiCmbBox.IsEnabled = false;
                facultateCmbBox.IsEnabled = false;

                studentCatalogsViewSource.View.Refresh();
            }
        }
        private void btnNewStudent_Click(object sender, RoutedEventArgs e)
        {

            
            action = ActionState.New;

            btnNewStudent.IsEnabled = false;
            btnEditStudent.IsEnabled = false;
            btnDeleteStudent.IsEnabled = false;
            //btnSaveStudent.IsEnabled = true;
            //btnCancelStudent.IsEnabled = true;
            studentDataGrid.IsEnabled = false;
            btnPrevStudent.IsEnabled = false;
            btnNextStudent.IsEnabled = false;
            nrTelTextBox.IsEnabled = true;
            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;

            //BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            //BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            //BindingOperations.ClearBinding(nrTelTextBox, TextBox.TextProperty);

            //SetValidationBindingStudent();
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(nrTelTextBox, TextBox.TextProperty);

            numeTextBox.Text = "";
            prenumeTextBox.Text = "";
            nrTelTextBox.Text = "";
        }
        private void btnNewFacultate_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;

            btnNewFacultate.IsEnabled = false;
            btnEditFacultate.IsEnabled = false;
            btnDeleteFacultate.IsEnabled = false;
            //btnSaveFacultate.IsEnabled = true;
            //btnCancelFacultate.IsEnabled = true;
            facultateDataGrid.IsEnabled = false;
            btnPrevFacultate.IsEnabled = false;
            btnNextFacultate.IsEnabled = false;
            nrTelFacultateTextBox.IsEnabled = true;
            numeFacultateTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeFacultateTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(nrTelFacultateTextBox, TextBox.TextProperty);

            //SetValidationBindingFacultate();

            nrTelFacultateTextBox.Text = "";
            numeFacultateTextBox.Text = "";
        }
        private void btnNewCatalog_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;

            btnNewCatalog.IsEnabled = false;        
            btnDeleteCatalog.IsEnabled = false;
            btnSaveCatalog.IsEnabled = true;
            btnCancelCatalog.IsEnabled = true;
            catalogDataGrid.IsEnabled = false;
            facultateCmbBox.IsEnabled = true;
            studentiCmbBox.IsEnabled = true;
        }
        private void btnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

            btnNewStudent.IsEnabled = false;
            btnEditStudent.IsEnabled = false;
            btnDeleteStudent.IsEnabled = false;
            //btnSaveStudent.IsEnabled = true;
            //btnCancelStudent.IsEnabled = true;
            studentDataGrid.IsEnabled = false;
            btnPrevStudent.IsEnabled = false;
            btnNextStudent.IsEnabled = false;
            nrTelTextBox.IsEnabled = true;
            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;

            //BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            //BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            //BindingOperations.ClearBinding(nrTelTextBox, TextBox.TextProperty);

            SetValidationBindingStudent();
        }
        private void btnEditFacultate_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;          

            btnNewFacultate.IsEnabled = false;
            btnEditFacultate.IsEnabled = false;
            btnDeleteFacultate.IsEnabled = false;
            //btnSaveFacultate.IsEnabled = true;
            //btnCancelFacultate.IsEnabled = true;
            facultateDataGrid.IsEnabled = false;
            btnPrevFacultate.IsEnabled = false;
            btnNextFacultate.IsEnabled = false;
            nrTelFacultateTextBox.IsEnabled = true;
            numeFacultateTextBox.IsEnabled = true;

            //BindingOperations.ClearBinding(numeFacultateTextBox, TextBox.TextProperty);
            //BindingOperations.ClearBinding(nrTelFacultateTextBox, TextBox.TextProperty);

            SetValidationBindingFacultate();
        }
       
        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            btnNewStudent.IsEnabled = false;
            btnEditStudent.IsEnabled = false;
            btnDeleteStudent.IsEnabled = false;
            //btnSaveStudent.IsEnabled = true;
            //btnCancelStudent.IsEnabled = true;
            studentDataGrid.IsEnabled = false;
            btnPrevStudent.IsEnabled = false;
            btnNextStudent.IsEnabled = false;
            nrTelTextBox.IsEnabled = false;
            numeTextBox.IsEnabled = false;
            prenumeTextBox.IsEnabled = false;
        }
        private void btnDeleteFacultate_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            btnNewFacultate.IsEnabled = false;
            btnEditFacultate.IsEnabled = false;
            btnDeleteFacultate.IsEnabled = false;
            //btnSaveFacultate.IsEnabled = true;
            //btnCancelFacultate.IsEnabled = true;
            studentDataGrid.IsEnabled = false;
            btnPrevFacultate.IsEnabled = false;
            btnNextFacultate.IsEnabled = false;
            nrTelFacultateTextBox.IsEnabled = false;
            numeFacultateTextBox.IsEnabled = false;

        }
        private void btnDeleteCatalog_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            btnNewCatalog.IsEnabled = false;
            btnDeleteCatalog.IsEnabled = false;
            btnSaveCatalog.IsEnabled = true;
            btnCancelCatalog.IsEnabled = true;
            catalogDataGrid.IsEnabled = false;
            facultateCmbBox.IsEnabled = false;
            studentiCmbBox.IsEnabled = false;
        }
        private void btnCancelStudent_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;

            btnNewStudent.IsEnabled = true;
            btnEditStudent.IsEnabled = true;
            btnDeleteStudent.IsEnabled = true;
            //btnSaveStudent.IsEnabled = false;
            //btnCancelStudent.IsEnabled = false;
            studentDataGrid.IsEnabled = true;
            btnPrevStudent.IsEnabled = true;
            btnNextStudent.IsEnabled = true;
            nrTelTextBox.IsEnabled = false;
            numeTextBox.IsEnabled = false;
            prenumeTextBox.IsEnabled = false;

            SetValidationBindingStudent();
            
        }
        private void btnCancelFacultate_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;

            btnNewFacultate.IsEnabled = true;
            btnEditFacultate.IsEnabled = true;
            btnDeleteFacultate.IsEnabled = true;
            //btnSaveFacultate.IsEnabled = false;
            //btnCancelFacultate.IsEnabled = false;
            facultateDataGrid.IsEnabled = true;
            btnPrevFacultate.IsEnabled = true;
            btnNextFacultate.IsEnabled = true;
            nrTelFacultateTextBox.IsEnabled = false;
            numeFacultateTextBox.IsEnabled = false;

            SetValidationBindingFacultate();
        }
        private void btnCancelCatalog_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;

            btnNewCatalog.IsEnabled = true;
            btnDeleteCatalog.IsEnabled = true;
            btnSaveCatalog.IsEnabled = false;
            btnCancelCatalog.IsEnabled = false;
            catalogDataGrid.IsEnabled = true;
            facultateCmbBox.IsEnabled = true;
            studentiCmbBox.IsEnabled = true;


        }
        private void btnPrevStudent_Click(object sender, RoutedEventArgs e)
        {
            studentViewSource.View.MoveCurrentToPrevious();
        }
        private void btnPrevFacultate_Click(object sender, RoutedEventArgs e)
        {
            facultateViewSource.View.MoveCurrentToPrevious();
        }
        private void btnNextStudent_Click(object sender, RoutedEventArgs e)
        {
            studentViewSource.View.MoveCurrentToNext();
        }
        private void btnNextFacultate_Click(object sender, RoutedEventArgs e)
        {
            facultateViewSource.View.MoveCurrentToNext();
        }
        
        private void BindDataGrid()
        {
            var queryOrder = from cat in ctx.Catalogs
                             join stu in ctx.Students on cat.studentId equals
                             stu.studentId
                             join fac in ctx.Facultates on cat.facultateId
                 equals fac.facultateId
                             select new
                             {   
                                 stu.nume,
                                 stu.prenume,
                                 fac.numeFacultate
                             };
            studentCatalogsViewSource.Source = queryOrder.ToList();
        }
        private void SetValidationBindingStudent()
        {
            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = studentViewSource;
            numeValidationBinding.Path = new PropertyPath("nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; 
            numeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding);
            
            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = studentViewSource;
            prenumeValidationBinding.Path = new PropertyPath("prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; 
            prenumeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding); 
            
            Binding nrTelValidationBinding = new Binding();
            nrTelValidationBinding.Source = studentViewSource;
            nrTelValidationBinding.Path = new PropertyPath("nrTel");
            nrTelValidationBinding.NotifyOnValidationError = true;
            nrTelValidationBinding.Mode = BindingMode.TwoWay;
            nrTelValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; 
            nrTelValidationBinding.ValidationRules.Add(new StringMinLengthValidatorNrTel());
            nrTelTextBox.SetBinding(TextBox.TextProperty, nrTelValidationBinding);
        }
        private void SetValidationBindingFacultate()
        {
            Binding numeFacultateValidationBinding = new Binding();
            numeFacultateValidationBinding.Source = facultateViewSource;
            numeFacultateValidationBinding.Path = new PropertyPath("numeFacultate");
            numeFacultateValidationBinding.NotifyOnValidationError = true;
            numeFacultateValidationBinding.Mode = BindingMode.TwoWay;
            numeFacultateValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numeFacultateValidationBinding.ValidationRules.Add(new StringNotEmpty());
            numeFacultateTextBox.SetBinding(TextBox.TextProperty, numeFacultateValidationBinding);

            Binding nrTelFacultateValidationBinding = new Binding();
            nrTelFacultateValidationBinding.Source = facultateViewSource;
            nrTelFacultateValidationBinding.Path = new PropertyPath("nrTelFacultate");
            nrTelFacultateValidationBinding.NotifyOnValidationError = true;
            nrTelFacultateValidationBinding.Mode = BindingMode.TwoWay;
            nrTelFacultateValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            nrTelFacultateValidationBinding.ValidationRules.Add(new StringMinLengthValidatorNrTel());
            nrTelFacultateTextBox.SetBinding(TextBox.TextProperty, nrTelFacultateValidationBinding);
        }

    }
    
}
