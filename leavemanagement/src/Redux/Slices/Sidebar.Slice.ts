import { createSlice,PayloadAction } from "@reduxjs/toolkit";
import { ISidebar } from "../../Types/Sidebar";


const initialState: ISidebar = {
    activeItem: null,
    openSubmenu: null,
    isExpanded: true,
    isHovered: false,
    isMobile: false,
    isMobileOpen: false,
}

const SidebarSlice = createSlice({
    name: "Sidebar",
    initialState: initialState,
    reducers: {
        setIsExpanded: (state) => {
            state.isExpanded = !state.isExpanded;
        },
        setIsMobile: (state, action: PayloadAction<boolean>) =>
        {
            state.isMobile = action.payload;
        },
        setIsMobileOpen: (state, action: PayloadAction<boolean>) => {
            state.isMobileOpen = action.payload;
        },
        setIsHovered: (state, action: PayloadAction<boolean>) => {
            state.isHovered = action.payload;
        },
        setActiveItem: (state, action: PayloadAction<string>) => {
            state.activeItem = action.payload;
        },
        toggleSidebar: (state) => {
            state.isExpanded = !state.isExpanded;
        },
        toggleMobileSidebar: (state) => {
            state.isMobileOpen = !state.isMobileOpen;
        },
        toggleSubmenu: (state, action: PayloadAction<string>) => {
            state.openSubmenu = state.openSubmenu === action.payload ? null : action.payload;            
        }

    },
});

export const { setIsExpanded, setActiveItem, setIsHovered, setIsMobile, setIsMobileOpen, toggleMobileSidebar, toggleSidebar,toggleSubmenu } = SidebarSlice.actions;
export default SidebarSlice;
