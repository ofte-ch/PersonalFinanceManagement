import { useAccounts } from "~/api/accounts/get-accounts";
const DashboardPage = () =>{
     const page = 1;
     const keyword = "";
     const {data:accounts} = useAccounts({page,size:10,keyword});
     console.log(accounts);
     return (
          <p>This is dashboard page</p>
     )
}
export default DashboardPage;