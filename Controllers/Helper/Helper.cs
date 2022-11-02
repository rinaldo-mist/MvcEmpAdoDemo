namespace MvcEmpAdoDemo.Controllers.Helper{
    public static class Helper {
        //Region
        public static readonly String REGION_ID = "ID";
        //EmployeeDataAcessLayer
        public static readonly String CONN_STRING_EMP = "Server=localhost;Database=master;Integrated Security=true;TrustServerCertificate=True";
        public static readonly String STD_ID = "0";
        public static readonly String SP_GET_ALL_EMP = "spGetAllEmployees";
        public static readonly String SP_ADD_EMP = "spAddEmployee";
        public static readonly String SP_GET_EMP_BY_ID = "spGetEmployee";
        public static readonly String SP_UPDATE_EMP_BY_ID = "spUpdateEmployee";
        public static readonly String SP_DELETE_EMP_BY_ID = "spDeleteEmployee";
        public static readonly String SP_GET_SALARY_EMP = "spGetTotalSalaryEmployeeByJabatan";
        public static String generateEmpId(String jabatan, String nama){
            return (REGION_ID+"E"+nama+"emp"+jabatan.Substring(0,1)+""+generateDateNow()).Substring(0,20);
        }
        public static int initDelStatus(){
            return 0;
        }
        public static String generateDateNow(){
            return DateTime.Now.ToString("yyyyMMdd").Replace('/', ' ');
        }

        public static int sumSalary(List<int> salary){
            int outRes = 0;
            foreach (int num in salary){
                outRes+=num;
            }
            return outRes;
        }

    }
}