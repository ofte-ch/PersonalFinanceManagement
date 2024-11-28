import { useQuery } from "@tanstack/react-query";
import { api } from "~/axios/api";

export const getAllTransactions = async (/*{_page, _size}*/) => {
    const response = await api.get(`/GiaoDich`);
    return response;
};

export const excuteGetAllTransactions = async(/*{_page, _size}*/) => {
    return useQuery({
        queryKey: ['GiaoDich'],
        queryFn: () => getAllTransactions(/*_page, _size*/),
    })
}
