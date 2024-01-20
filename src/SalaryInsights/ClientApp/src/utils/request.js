import axios from "axios";

import { Snackbar } from '@mui/material';

const axiosInstance = axios.create({
    timeout: 5000
})

// request interceptor
axiosInstance.interceptors.request.use(
    config => {
        if (store.getters.token) {
            config.headers['Authorization'] = "Bearer xxxx"
        }
        return config
    },
    error => {
        console.error(error)
        return Promise.reject(error)
    }
);

// response interceptor
axiosInstance.interceptors.response.use(
    response => {
        if (response.code !== 200) {
            Snackbar({
                open: true,
                message: response.Message,
                anchorOrigin: {
                    vertical: 'top',
                    horizontal: 'right'
                },
                autoHideDuration: 5 * 1000
            })
            return Promise.reject(new Error(response.Message || 'Error'))
        }

        return response.data;
    },
    error => {
        console.error('An error occurred while requesting api:' + error)
        Snackbar({
            open: true,
            message: error.message,
            anchorOrigin: {
                vertical: 'top',
                horizontal: 'right'
            },
            autoHideDuration: 5 * 1000
        })
        return Promise.reject(error)
    }
)

export default axiosInstance
