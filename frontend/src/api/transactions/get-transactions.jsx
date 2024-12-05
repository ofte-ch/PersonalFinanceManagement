import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getTransactions = async ({page="", size="", keyword="", codeTK=""}) => {
    const response = await api.get(`/GiaoDich`, {
        params: {page, size, keyword, codeTK},
    });

    return {
        data: response.data,
        totalCount: response.totalCount,
    };
    //response.data;
};

export const useGetTransactions = ({page, size, keyword, codeTK}) => {
    return useQuery({
        queryKey: page ? ['GiaoDich', { page, size, keyword, codeTK }] : ['GiaoDich'],
        queryFn: () => getTransactions({ page, size, keyword, codeTK }),
    })
}
