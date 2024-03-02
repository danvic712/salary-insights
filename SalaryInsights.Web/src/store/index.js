import { configureStore } from "@reduxjs/toolkit";

import * as actionTypes from './actions'

export const initialState = {
    locale: 'zh_CN'
};

const customizationReducer = (state = initialState, action) => {
    let id;
    switch (action.type) {
        case actionTypes.CHANGE_LOCALE:
            id = action.id;
            return {
                ...state,
                locale: action.locale
            };
        default:
            return state;
    }
};

export default configureStore({
    reducer: {
        customization: customizationReducer
    },
})