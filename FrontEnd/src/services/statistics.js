import * as axios from 'axios';
import { setExpenseAmountPerCategory } from '../app/statisticsSlice';

const axiosInstance = axios.create({
    baseURL: `${process.env.REACT_APP_BASE_URL}/statistics`,
});

axiosInstance.interceptors.request.use((config) => {
    config.headers = { authorization: 'Bearer ' + sessionStorage.getItem('token') };
    return config;
});

export const getExpensesPerCategory = async (dispatch) => {
    try {
        const { data } = await axiosInstance.get();
        dispatch(setExpenseAmountPerCategory(data));
    } catch (error) {
        console.log(error);
    }
}