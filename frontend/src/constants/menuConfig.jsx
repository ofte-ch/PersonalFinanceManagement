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
               icon:<DashboardOutlined />,
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
               path:"/dashboard/statistics",
          }
     ];
     return menuItems;
}

export default MenuConfig;