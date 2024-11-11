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
               label:"Transactions",
               icon:<TransactionOutlined />,
               path:"/transactions",
          },
          {
               key:"2",
               label:"Accounts",
               icon:<BankOutlined />,
               path:"/accounts",
          },
          {
               key:"3",
               label:"Statistics",
               icon:<PieChartOutlined />,
               path:"/statistics",
          }
     ];
     return menuItems;
}

export default MenuConfig;