import { api } from "~/axios/api";
import { useQuery } from "@tanstack/react-query";

export const getAllTransactions = async () => {
    const response = await api.get("/GiaoDich");
    return response;
};

