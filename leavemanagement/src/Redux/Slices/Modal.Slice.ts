import { createSlice } from "@reduxjs/toolkit/react"

const ModalSlice = createSlice({
    name: "modal",
    initialState: {isOpen:false},
    reducers: {
        openModal: (state) => {
            state.isOpen = true;
        },
        closeModal: (state) => {
            state.isOpen = false
        },
        toggelModal: (state) => {
            state.isOpen = !state.isOpen;
        }        
    }
})
export const { openModal, closeModal, toggelModal } = ModalSlice.actions;
export default ModalSlice;