import axiosInstance from '../utils/request';

const getPortfolios = (params) => {
    return axiosInstance({
        url: '/api/portfolios',
        method: 'get',
        params: params
    });
}

const getPortfolioById = (id) => {
    return axiosInstance({
        url: `/api/portfolios/${id}`,
        method: 'get'
    });
}

export default PortfolioEndpoints = {
    getPortfolios,
    getPortfolioById
};