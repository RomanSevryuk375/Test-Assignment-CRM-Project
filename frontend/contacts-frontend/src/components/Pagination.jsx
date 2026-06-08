export default function Pagination({ currentPage, total, pageSize, onPageChange }) {
    const totalPages = Math.ceil(total / pageSize) || 1;

    return (
        <div className="flex justify-between items-center mt-4">
            <span className="text-sm text-gray-600">
                Страница {currentPage} из {totalPages} (Всего: {total})
            </span>
            <div className="flex space-x-2">
                <button 
                    onClick={() => onPageChange(currentPage - 1)}
                    disabled={currentPage <= 1}
                    className="px-3 py-1 bg-white border border-gray-300 rounded-md text-sm font-medium hover:bg-gray-50 disabled:opacity-50"
                >
                    Назад
                </button>
                <button 
                    onClick={() => onPageChange(currentPage + 1)}
                    disabled={currentPage >= totalPages}
                    className="px-3 py-1 bg-white border border-gray-300 rounded-md text-sm font-medium hover:bg-gray-50 disabled:opacity-50"
                >
                    Вперед
                </button>
            </div>
        </div>
    );
}