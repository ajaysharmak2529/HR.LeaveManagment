import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import tailwindcss from '@tailwindcss/vite'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(), tailwindcss()],
    server: {
        port: 65523,
    },
    resolve: {
        alias: {
            "@": "src/",
            "@pages": "src/pages/",
            "@components": "src/Components/",
            "@store": "src/Redux/Store/",
            "@services": "src/Services",
            "@types": "src/Types",
        }
    }
})
