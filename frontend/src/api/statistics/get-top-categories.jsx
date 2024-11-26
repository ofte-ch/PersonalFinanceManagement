export const getTopCategories = (data, key, limit = 5) => {
    //loc cac muc co gia tri > 0
    const filteredData = data.filter((item) => item[key] >= 0);

    //sap xep theo thu tu giam dan
    const sortedData = filteredData.sort((a, b) => b[key] - a[key]);

    //lay ra limit phan tu dau tien
    return sortedData.slice(0, limit);
}