const BASE_URL = '/api/contacts';

export const getContacts = async (page, pageSize) => {
    const res = await fetch(`${BASE_URL}?page=${page}&pageSize=${pageSize}`);
    if (!res.ok) throw new Error('Ошибка загрузки');
    return res.json();
};

export const saveContact = async (contact, isEdit) => {
    const url = isEdit ? `${BASE_URL}/${contact.id}` : BASE_URL;
    const method = isEdit ? 'PUT' : 'POST';
    const res = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(contact)
    });
    return res;
};

export const deleteContact = (id) => fetch(`${BASE_URL}/${id}`, { method: 'DELETE' });