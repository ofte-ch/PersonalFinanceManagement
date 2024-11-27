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
               label:"Transactions",
               icon:<TransactionOutlined />,
               path:"/dashboard/transactions",
          },
          {
               key:"3",
               label:"Statistics",
               icon:<PieChartOutlined />,
               children: [
                    {
                         key:"3.1",
                         label:"By Transaction Type",
                         path:"/dashboard/statistics/by-transaction-type",
                    },
                    {
                         key:"3.2",
                         label:"By Account",
                         path:"/dashboard/statistics/by-account",
                    }
               ]
          }
     ];
     return menuItems;
}

export default MenuConfig;