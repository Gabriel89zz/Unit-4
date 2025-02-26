using System.Windows.Forms.DataVisualization.Charting;
namespace Unit_4
{
    public partial class Form1 : Form
    {
        private List<Employee> employees = new List<Employee>();
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            if (chartSalary.ChartAreas.Count == 0)
            {
                chartSalary.ChartAreas.Add(new ChartArea("Default"));
            }

            chartSalary.Titles.Add("Salary Distribution by Department");
            chartSalary.ChartAreas[0].AxisX.Title = "Department";
            chartSalary.ChartAreas[0].AxisY.Title = "Salary";
        }

        // Add employee to the list
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string department = txtDepartment.Text;
                decimal salary = decimal.Parse(txtSalary.Text);

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(department))
                {
                    throw new Exception("Name and Department fields cannot be empty.");
                }

                employees.Add(new Employee { Name = name, Department = department, Salary = salary });
                UpdateTable();
                UpdateChart();
                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid salary format. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update the DataGridView with employee data
        private void UpdateTable()
        {
            dataGridViewEmployees.DataSource = null;
            dataGridViewEmployees.DataSource = employees;
        }

        // Update the chart with salary distribution
        private void UpdateChart()
        {
            chartSalary.Series["Salary"].Points.Clear();

            var departmentSalaries = new Dictionary<string, decimal>();
            foreach (var employee in employees)
            {
                if (departmentSalaries.ContainsKey(employee.Department))
                {
                    departmentSalaries[employee.Department] += employee.Salary;
                }
                else
                {
                    departmentSalaries.Add(employee.Department, employee.Salary);
                }
            }

            foreach (var dept in departmentSalaries)
            {
                chartSalary.Series["Salary"].Points.AddXY(dept.Key, dept.Value);
            }
        }

        // Clear input fields
        private void ClearFields()
        {
            txtName.Clear();
            txtDepartment.Clear();
            txtSalary.Clear();
        }
    }

    // Employee class to store employee details
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}

