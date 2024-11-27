import { useAccounts } from "~/api/accounts/get-accounts";
import LoginWrapper from "../auth/LoginRegis";
const DashboardPage = () =>{
     const page = 1;
     const keyword = "";
     const {data:accounts} = useAccounts({page,size:10,keyword});
     console.log(accounts);
     return (
          <LoginWrapper />
     )
}
export default DashboardPage;