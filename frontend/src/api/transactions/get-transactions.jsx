import { useQuery } from "@tanstack/react-query";
import { api } from "~/axios/api";

export const getAllTransactions = async ({currentPage, pageSize, keyword="", codeTK=""}) => {
    const response = await api.get(`/GiaoDich`, {
        params: {currentPage, pageSize, keyword, codeTK},
    });
    console.log(response);
    return response.data;
};

export const useGetTransactions = ({currentPage, pageSize, keyword="", codeTK=""}) => {
    return useQuery({
        queryKey: ['transactions', {currentPage, pageSize, keyword, codeTK}],
        queryFn:() => getAllTransactions(currentPage, pageSize, keyword, codeTK),
    })
}
