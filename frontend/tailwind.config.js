/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily :{
        'bad-script': ['"Bad Script"', 'cursive'], // Thêm font Bad Script
        'poppins': ['Poppins', 'sans-serif'],       // Thêm font Poppins
      }
    },
  },
  plugins: [],
}

