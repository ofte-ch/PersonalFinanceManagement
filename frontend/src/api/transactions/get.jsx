import { api } from "~/axios/api";

export const getAllTransactions = async () => {
    const response = await api.get("/GiaoDich");
    return response;
};

export const getCurrentMaxID = async () => {
    const ID = await api.get("/GiaoDich")
}