export default function ContactTable({ contacts, onEdit, onDelete }) {
    return (
        <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">
            <table className="min-w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                    <tr>
                        <th className="px-6 py-3 text-left text-xs font-semibold text-gray-500 uppercase">Имя</th>
                        <th className="px-6 py-3 text-left text-xs font-semibold text-gray-500 uppercase">Телефон</th>
                        <th className="px-6 py-3 text-left text-xs font-semibold text-gray-500 uppercase">Должность</th>
                        <th className="px-6 py-3 text-left text-xs font-semibold text-gray-500 uppercase">Дата рождения</th>
                        <th className="px-6 py-3 text-right text-xs font-semibold text-gray-500 uppercase">Действия</th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-gray-200 bg-white">
                    {contacts.length === 0 ? (
                        <tr>
                            <td colSpan="5" className="px-6 py-8 text-center text-gray-500">Список контактов пуст</td>
                        </tr>
                    ) : (
                        contacts.map(c => (
                            <tr key={c.id} className="hover:bg-gray-50 transition">
                                <td className="px-6 py-4 font-medium text-gray-900">{c.name}</td>
                                <td className="px-6 py-4 text-gray-600">{c.mobilePhone}</td>
                                <td className="px-6 py-4 text-gray-500">{c.jobTitle || '—'}</td>
                                <td className="px-6 py-4 text-gray-500">
                                    {new Date(c.birthDate).toLocaleDateString('ru-RU')}
                                </td>
                                <td className="px-6 py-4 text-right space-x-3">
                                    <button onClick={() => onEdit(c)} className="text-indigo-600 hover:text-indigo-900 font-medium">Ред.</button>
                                    <button onClick={() => onDelete(c.id)} className="text-red-600 hover:text-red-900 font-medium">Удал.</button>
                                </td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>
        </div>
    );
}