import axios from "axios";

import { Notification } from "@douyinfe/semi-ui";

const httpClient = axios.create({
    timeout: 5000
});

// 请求前拦截器
httpClient.interceptors.request.use(
    function (config) {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

// 请求后拦截器
httpClient.interceptors.response.use(
    function (response) {
        if (response.status === 200) {
            return response.data; // 返回请求响应内容
        } else {
            // 处理其他状态码
            return response.data;
        }
    },
    function (error) {
        if (error.response.status === 401) {
            // 重定向到登录页面
            window.location.href = '/login';
        } else {
            // 提示错误信息
            console.error('请求错误:', error);
            return Promise.reject(error);
        }
    }
);

export default httpClient;