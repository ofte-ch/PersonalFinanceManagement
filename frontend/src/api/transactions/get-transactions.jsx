import { useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getTransactions = async ({page="", size="", keyword="", maTaiKhoan=""}) => {
    const response = await api.get(`/GiaoDich`, {
        params: {page, size, keyword, maTaiKhoan},
    });

    return {
        data: response.data,
        totalCount: response.totalCount,
    };
    //response.data;
};

export const useGetTransactions = ({page, size, keyword, maTaiKhoan}) => {
    return useQuery({
        queryKey: ['GiaoDich', { page, size, keyword, maTaiKhoan }],
        queryFn: () => getTransactions({ page, size, keyword, maTaiKhoan }),
    })
}
