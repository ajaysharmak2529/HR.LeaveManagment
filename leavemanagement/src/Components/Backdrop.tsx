import { useSelector,useDispatch } from "react-redux";
import { toggleMobileSidebar } from "../Redux/Slices/Sidebar.Slice";
import { RootState } from "../Redux/Store/Store";

const Backdrop: React.FC = () => {
    const { isMobileOpen } = useSelector((state: RootState) => state.Sidebar);
    const dispatch = useDispatch();

  if (!isMobileOpen) return null;

  return (
    <div
          className="fixed inset-0 z-40 bg-gray-900 bg-opacity-50/50 lg:hidden"
          onClick={() => { dispatch(toggleMobileSidebar()) }}
    />
  );
};

export default Backdrop;
