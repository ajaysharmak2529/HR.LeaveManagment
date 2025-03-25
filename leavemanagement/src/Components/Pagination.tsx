import { FaLessThan, FaGreaterThan } from "react-icons/fa";

interface PaginationProps {
    hasPreviousPage: boolean,
    hasNextPage: boolean
    totalCount: number;
    pageSize: number;
    activePage: number;
    onPrevious: () => void;
    onNext: () => void;
    onSetPage: (page:number) => void;
}

const Pagination = ({ activePage, pageSize, hasNextPage, hasPreviousPage, totalCount, onNext, onPrevious, onSetPage }: PaginationProps) => {


    const pages = [];
    const maxVisiblePages = 5;
    const totalPages = Math.ceil(totalCount / pageSize);
    let startPage = Math.max(1, activePage - Math.floor(maxVisiblePages / 2));
    let endPage = startPage + maxVisiblePages - 1;

    if (endPage > totalPages) {
        endPage = totalPages;
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
        pages.push(
            <li key={i} className="px-2">
                <button
                    className={`w-9 h-9 rounded-md border hover:border-cyan-500 hover:text-indigo-500 ${activePage === i ? 'bg-indigo-500 text-white' : ''}`}
                    onClick={() => onSetPage(i)}
                >
                    {i}
                </button>
            </li>
        );
    }


    return (
        <div className="inline-flex rounded-xl">
            <ul className="flex items-center">
                <li className="px-2">
                    <button onClick={onPrevious}
                        aria-disabled="true"
                        disabled={!hasPreviousPage}
                        className="w-9 h-9 flex items-center justify-center rounded-md border disabled"
                    >
                        <span>
                            <FaLessThan />
                        </span>
                    </button>
                </li>
                {pages}
                <li className="px-2">
                    <button onClick={onNext} disabled={!hasNextPage}
                        aria-disabled="false"
                        className="w-9 h-9 flex items-center justify-center rounded-md border hover:text-indigo-500"
                    >
                        <span>
                            <FaGreaterThan />
                        </span>
                    </button>
                </li>
            </ul>
        </div>
    );
};

export default Pagination;
