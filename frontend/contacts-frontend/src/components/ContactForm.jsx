export default function ContactForm({ data, errors, onChange, onSubmit, onClose, isEdit, maxDate }) {
    const inputClassName = (fieldName) =>
        `w-full border p-2 rounded ${errors[fieldName] ? 'border-red-500' : 'border-gray-300'}`;

    return (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex items-center justify-center p-4 z-50">
            <div className="bg-white rounded-xl shadow-lg max-w-md w-full p-6">
                <h3 className="text-lg font-bold mb-4">{isEdit ? 'Редактирование' : 'Создание'}</h3>
                <form onSubmit={onSubmit} noValidate>
                    <div className="mb-4">
                        <label className="block text-sm font-medium">Имя *</label>
                        <input
                            name="name"
                            value={data.name}
                            onChange={onChange}
                            className={inputClassName('Name')}
                            maxLength={200}
                            required
                        />
                        {errors.Name && <p className="text-red-500 text-xs">{errors.Name[0]}</p>}
                    </div>
                    <div className="mb-4">
                        <label className="block text-sm font-medium">Телефон *</label>
                        <input
                            name="mobilePhone"
                            value={data.mobilePhone}
                            onChange={onChange}
                            className={inputClassName('MobilePhone')}
                            maxLength={50}
                            pattern="^\+?[0-9\s\-()]+$"
                            required
                        />
                        {errors.MobilePhone && <p className="text-red-500 text-xs">{errors.MobilePhone[0]}</p>}
                    </div>
                    <div className="mb-4">
                        <label className="block text-sm font-medium">Должность</label>
                        <input
                            name="jobTitle"
                            value={data.jobTitle}
                            onChange={onChange}
                            className={inputClassName('JobTitle')}
                            maxLength={150}
                        />
                        {errors.JobTitle && <p className="text-red-500 text-xs">{errors.JobTitle[0]}</p>}
                    </div>
                    <div className="mb-4">
                        <label className="block text-sm font-medium">Дата рождения *</label>
                        <input
                            type="date"
                            name="birthDate"
                            value={data.birthDate}
                            onChange={onChange}
                            className={inputClassName('BirthDate')}
                            max={maxDate}
                            required
                        />
                        {errors.BirthDate && <p className="text-red-500 text-xs">{errors.BirthDate[0]}</p>}
                    </div>
                    
                    <div className="flex justify-end space-x-2 mt-6">
                        <button type="button" onClick={onClose} className="bg-gray-100 px-4 py-2 rounded-lg">Отмена</button>
                        <button type="submit" className="bg-indigo-600 text-white px-4 py-2 rounded-lg">Сохранить</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
