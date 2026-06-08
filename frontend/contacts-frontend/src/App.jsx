import { useState, useEffect, useCallback } from 'react';
import ContactTable from './components/ContactTable';
import Pagination from './components/Pagination';
import ContactForm from './components/ContactForm';
import * as api from './services/api';

const PAGE_SIZE = 5;
const MAX_NAME_LENGTH = 200;
const MAX_MOBILE_PHONE_LENGTH = 50;
const MAX_JOB_TITLE_LENGTH = 150;
const PHONE_PATTERN = /^\+?[0-9\s\-()]+$/;

const getToday = () => new Date().toISOString().slice(0, 10);

const validateContact = ({ name, mobilePhone, jobTitle, birthDate }) => {
    const validationErrors = {};
    const trimmedName = name.trim();
    const trimmedPhone = mobilePhone.trim();
    const trimmedJobTitle = jobTitle.trim();

    if (!trimmedName) {
        validationErrors.Name = ['Введите имя.'];
    } else if (trimmedName.length > MAX_NAME_LENGTH) {
        validationErrors.Name = [`Имя не должно быть длиннее ${MAX_NAME_LENGTH} символов.`];
    }

    if (!trimmedPhone) {
        validationErrors.MobilePhone = ['Введите телефон.'];
    } else if (trimmedPhone.length > MAX_MOBILE_PHONE_LENGTH) {
        validationErrors.MobilePhone = [`Телефон не должен быть длиннее ${MAX_MOBILE_PHONE_LENGTH} символов.`];
    } else if (!PHONE_PATTERN.test(trimmedPhone)) {
        validationErrors.MobilePhone = ['Телефон может содержать только цифры, пробелы, +, -, скобки.'];
    }

    if (trimmedJobTitle.length > MAX_JOB_TITLE_LENGTH) {
        validationErrors.JobTitle = [`Должность не должна быть длиннее ${MAX_JOB_TITLE_LENGTH} символов.`];
    }

    if (!birthDate) {
        validationErrors.BirthDate = ['Укажите дату рождения.'];
    } else if (Number.isNaN(Date.parse(`${birthDate}T00:00:00.000Z`))) {
        validationErrors.BirthDate = ['Укажите корректную дату рождения.'];
    } else if (birthDate > getToday()) {
        validationErrors.BirthDate = ['Дата рождения не может быть в будущем.'];
    }

    return validationErrors;
};

const toServerErrorsKey = (fieldName) => ({
    name: 'Name',
    mobilePhone: 'MobilePhone',
    jobTitle: 'JobTitle',
    birthDate: 'BirthDate',
}[fieldName]);

export default function App() {
    const [contacts, setContacts] = useState([]);
    const [totalCount, setTotalCount] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);

    const [isOpen, setIsOpen] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false);
    const [editingId, setEditingId] = useState(null);

    const [formData, setFormData] = useState({ name: '', mobilePhone: '', jobTitle: '', birthDate: '' });
    const [errors, setErrors] = useState({});

    const loadData = useCallback(async () => {
        try {
            const data = await api.getContacts(currentPage, PAGE_SIZE);
            setContacts(data.items || []);
            setTotalCount(data.totalCount || 0);
        } catch (err) {
            console.error(err);
        }
    }, [currentPage]);

    useEffect(() => {
        let isActive = true;

        api.getContacts(currentPage, PAGE_SIZE)
            .then((data) => {
                if (!isActive) return;
                setContacts(data.items || []);
                setTotalCount(data.totalCount || 0);
            })
            .catch((err) => {
                if (isActive) console.error(err);
            });

        return () => {
            isActive = false;
        };
    }, [currentPage]);

    const handleOpenModal = (contact = null) => {
        setErrors({});
        if (contact) {
            setFormData({
                name: contact.name,
                mobilePhone: contact.mobilePhone,
                jobTitle: contact.jobTitle || '',
                birthDate: contact.birthDate.substring(0, 10)
            });
            setIsEditMode(true);
            setEditingId(contact.id);
        } else {
            setFormData({ name: '', mobilePhone: '', jobTitle: '', birthDate: '' });
            setIsEditMode(false);
            setEditingId(null);
        }
        setIsOpen(true);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const validationErrors = validateContact(formData);
        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }
        
        const payload = {
            id: editingId,
            name: formData.name.trim(),
            mobilePhone: formData.mobilePhone.trim(),
            jobTitle: formData.jobTitle.trim(),
            birthDate: new Date(`${formData.birthDate}T00:00:00.000Z`).toISOString()
        };

        try {
            const response = await api.saveContact(payload, isEditMode);
            if (response.ok) {
                setIsOpen(false);
                loadData();
            } else {
                const data = await response.json();
                if (data.errors) setErrors(data.errors);
            }
        } catch (err) {
            console.error(err);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Удалить контакт?")) {
            await api.deleteContact(id);
            loadData();
        }
    };

    return (
        <div className="container mx-auto px-4 py-8 max-w-4xl">
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-bold text-gray-900">Контакты</h1>
                <button 
                    onClick={() => handleOpenModal()}
                    className="bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-lg font-medium transition"
                >
                    + Добавить
                </button>
            </div>

            <ContactTable 
                contacts={contacts} 
                onEdit={handleOpenModal} 
                onDelete={handleDelete} 
            />

            <Pagination 
                currentPage={currentPage} 
                total={totalCount} 
                pageSize={PAGE_SIZE}
                onPageChange={setCurrentPage} 
            />

            {isOpen && (
                <ContactForm 
                    data={formData}
                    errors={errors}
                    isEdit={isEditMode}
                    maxDate={getToday()}
                    onChange={(e) => {
                        const errorKey = toServerErrorsKey(e.target.name);
                        setFormData({...formData, [e.target.name]: e.target.value});
                        if (errorKey && errors[errorKey]) {
                            setErrors((currentErrors) => {
                                const nextErrors = { ...currentErrors };
                                delete nextErrors[errorKey];
                                return nextErrors;
                            });
                        }
                    }}
                    onSubmit={handleSubmit}
                    onClose={() => setIsOpen(false)}
                />
            )}
        </div>
    );
}
