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
               label:"Accounts",
               icon:<BankOutlined />,
               path:"/dashboard/accounts",
          },
          {
                key:"2",
                label:"Account Types",
                icon:<BankOutlined />,
                path: "/dashboard/account-types",
          },
          {
               key:"3",
               label:"Transactions",
               icon:<TransactionOutlined />,
               path:"/dashboard/transactions",
          },
          {
               key:"4",
               label:"Statistics",
               icon:<PieChartOutlined />,
               children: [
                    {
                         key:"4.1",
                         label:"By Transaction Type",
                         path:"/dashboard/statistics/by-transaction-type",
                    },
                    {
                         key:"4.2",
                         label:"By Account",
                         path:"/dashboard/statistics/by-account",
                    }
               ]
          }
     ];
     return menuItems;
}

export default MenuConfig;