import { createSlice } from '@reduxjs/toolkit';

export const languageSlice = createSlice({
    name: 'language',
    initialState: {
        currentLanguage: 'zh_CN', // Default language
    },
    reducers: {
        setLanguage: (state, action) => {
            state.currentLanguage = action.payload;
        },
    },
});

export const { setLanguage } = languageSlice.actions;

export const selectLanguage = (state) => state.language.currentLanguage;

export default languageSlice.reducer;
