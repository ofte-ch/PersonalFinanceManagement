import {
     DashboardOutlined,
     BankOutlined,
     TransactionOutlined,
     PieChartOutlined
   } from "@ant-design/icons";

const MenuConfig = () => {
     const menuItems = [
          {
               key:"0",
               label:"Dashboard",
               icon:<DashboardOutlined/>,
               path:"/dashboard",

          },
          {
               key:"1",
               label:"Tài khoản",
               icon:<BankOutlined />,
               path:"/dashboard/accounts",
          },
          {
                key:"2",
                label:"Loại tài khoản",
                icon:<BankOutlined />,
                path: "/dashboard/account-types",
          },
          {
               key:"3",
               label:"Giao dịch",
               icon:<TransactionOutlined />,
               path:"/dashboard/transactions",
          },
          {
               key:"4",
               label:"Thống kê",
               icon:<PieChartOutlined />,
               children: [
                    {
                         key:"4.1",
                         label:"Theo loại giao dịch",
                         path:"/dashboard/statistics/by-transaction-type",
                    },
                    {
                         key:"4.2",
                         label:"Theo tài khoản",
                         path:"/dashboard/statistics/by-account",
                    }
               ]
          }
     ];
     return menuItems;
}

export default MenuConfig;