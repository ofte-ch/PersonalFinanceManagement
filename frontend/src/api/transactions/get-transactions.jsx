import { queryOptions,useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getTransactions = async ({page="", size="", keyword="", maTaiKhoan=""}) => {
    const response = await api.get(`/transactions`, {
        params: {page, size, keyword, maTaiKhoan},
    });
    return response;
};
export const getTransactionsQueryOptions = ({ page, size, keyword,maTaiKhoan }) => {
    return queryOptions({
      queryKey: page ? ["transactions", { page, size, keyword,maTaiKhoan }] : ["transactions"],
      queryFn: () => getTransactions({ page, size, keyword,maTaiKhoan }),
    });
  };

export const useGetTransactions = ({queryConfig,page, size, keyword, maTaiKhoan}) => {
    return useQuery({
        ...getTransactionsQueryOptions({ page, size, keyword, maTaiKhoan }),
        ...queryConfig,
    })
}
