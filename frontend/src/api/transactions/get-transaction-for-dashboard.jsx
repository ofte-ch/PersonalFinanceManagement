import { queryOptions,useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getTransactions = async ({page="", size="", TuNgay="", DenNgay=""}) => {
    const response = await api.get(`/transactions/by-date-range`, {
        params: {page, size, TuNgay, DenNgay},
    });
    return response;
};
export const getTransactionsQueryOptions = ({ page, size, TuNgay, DenNgay }) => {
    return queryOptions({
      queryKey: page ? ["transactions", { page, size, TuNgay,DenNgay }] : ["transactions"],
      queryFn: () => getTransactions({ page, size, TuNgay,DenNgay }),
    });
  };

export const useGetTransactionsByDateRange = ({queryConfig,page, size, TuNgay, DenNgay}) => {
    return useQuery({
        ...getTransactionsQueryOptions({ page, size, TuNgay, DenNgay }),
        ...queryConfig,
    })
}
